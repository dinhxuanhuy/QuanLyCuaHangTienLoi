using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SqlAgent
    {
        // Thông tin cấu hình (Nên đưa vào Config file sau này)
        private readonly string ApiKey =
            Environment.GetEnvironmentVariable("GROQ_API_KEY")
            ?? throw new InvalidOperationException("GROQ_API_KEY is not set");
        private const string ModelId = "llama-3.3-70b-versatile";

        private static SqlAgent instance = null;

        // Đổi tên thành _kernel để tránh nhầm lẫn với biến cục bộ
        private readonly Kernel _kernel;
        private readonly IChatCompletionService _chatService;

        private SqlAgent()
        {
            // 1. Tạo Builder
            var builder = Kernel.CreateBuilder();

            // 2. Cấu hình Groq API
            builder.AddOpenAIChatCompletion(
                modelId: ModelId,
                apiKey: ApiKey,
                httpClient: new System.Net.Http.HttpClient { BaseAddress = new Uri("https://api.groq.com/openai/v1") }
            );

            // 3. Build Kernel và GÁN VÀO BIẾN CỦA CLASS (QUAN TRỌNG NHẤT)
            // LỖI CŨ CỦA BẠN: var kernel = ... (Sai, vì nó tạo biến tạm)
            _kernel = builder.Build();

            // 4. Cấu hình và thêm Plugin DAL
            var dalInstance = DAL.Instance;
            // Đảm bảo Connection String đúng với máy tính của bạn
            dalInstance.connectionString = "Data Source=localhost;Initial Catalog=QuanLyCuaHangTienLoi;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            _kernel.Plugins.AddFromObject(dalInstance, pluginName: "DatabasePlugin");

            // 5. Lấy dịch vụ Chat
            _chatService = _kernel.GetRequiredService<IChatCompletionService>();
        }

        public static SqlAgent Instance
        {
            get
            {
                if (instance == null) instance = new SqlAgent();
                return instance;
            }
        }

        public async Task<string> GetAnswer(string question)
        {
            var chat = new ChatHistory();
            chat.AddSystemMessage(@"
                ### VAI TRÒ VÀ NGỮ CẢNH
                Bạn là Trợ lý AI Thông minh (Smart Store Assistant) tích hợp trong Phần mềm Quản lý Cửa hàng Tiện lợi.
                Đây là sản phẩm Đồ án Môn Công nghệ Thông tin tâm huyết được phát triển bởi nhóm sinh viên:
                1. Trần Huỳnh Chí Nguyên (MSSV: 23110136)
                2. Lâm Khánh Duy (MSSV: 23110084)
                3. Đinh Xuân Huy (MSSV: 23110102)

                ### NHIỆM VỤ CHÍNH
                Nhiệm vụ của bạn là hỗ trợ người quản lý tra cứu thông tin nhanh chóng bằng tiếng Việt.
                Bạn KHÔNG được tự bịa ra số liệu. Bạn BẮT BUỘC phải thực hiện quy trình sau:
                1. Phân tích câu hỏi của người dùng để hiểu họ cần dữ liệu gì.
                2. Sử dụng công cụ (DatabasePlugin) để truy vấn cơ sở dữ liệu.
                3. Dựa trên kết quả trả về, viết câu trả lời thân thiện.

                ### QUY TẮC ỨNG XỬ
                - **Ngôn ngữ:** Tiếng Việt.
                - **Thái độ:** Thân thiện, chuyên nghiệp.
                - **Xử lý lỗi:** Nếu không có dữ liệu, hãy xin lỗi và báo không tìm thấy.
            ");

            chat.AddUserMessage(question);

            // Cấu hình để AI tự động gọi hàm (Tool Calling)
            var settings = new OpenAIPromptExecutionSettings()
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
                Temperature = 0.1, // Nhiệt độ thấp để chính xác hơn
            };

            try
            {
                // Truyền _kernel (đã có plugin) vào đây
                var response = await _chatService.GetChatMessageContentAsync(chat, settings, _kernel);
                return response.Content ?? "Xin lỗi, tôi không có câu trả lời.";
            }
            catch (Exception ex)
            {
                return $"Đã xảy ra lỗi hệ thống: {ex.Message}";
            }
        }
    }
}