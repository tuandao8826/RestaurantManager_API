using RestaurantManager_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace RestaurantManager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DishController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Select * from Dishs";

            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("RestaurantManager");
            SqlDataReader myReader;

            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    sqlConnection.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Dish dish)
        {
            string query = @"Insert into Dishs(Name, MenuId, DateCreate, Image) values" +
                $"(" +
                $"N'{dish.Name}'," +
                $"{dish.MenuId}," +
                $"'{dish.DateCreate}'," +
                $"'{dish.Image}'" +
                $")";

            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("RestaurantManager");
            SqlDataReader myReader;

            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    sqlConnection.Close();
                }
            }

            return new JsonResult("Thêm mới thành công.");
        }

        [HttpPut]
        public JsonResult Put(Dish dish)
        {
            string query = @"Update Dishs" +
                $" Set " +
                $" Name = N'{dish.Name}'," +
                $" MenuId = {dish.MenuId}," +
                $" DateCreate = '{dish.DateCreate}'," +
                $" Image = '{dish.Image}'" +
                $" Where DishId = {dish.DishId}";

            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("RestaurantManager");
            SqlDataReader myReader;

            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    sqlConnection.Close();
                }
            }

            return new JsonResult("Cập nhật thành công.");
        }

        [HttpDelete("{dishId}")]
        public JsonResult Delete(int dishId)
        {
            string query = @"Delete from Dishs" +
                $" Where DishId = {dishId}";

            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("RestaurantManager");
            SqlDataReader myReader;

            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    sqlConnection.Close();
                }
            }

            return new JsonResult("Xóa thành công.");
        }

        // Khai báo phương thức SaveFile, nhận đối tượng Dish làm tham số
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                // Nhận dữ liệu từ yêu cầu HTTP
                var httpRequest = Request.Form;

                // Lấy ra tệp tin được gửi trong yêu cầu
                var postFile = httpRequest.Files[0];

                // Lấy ra tên của tệp tin
                string fileName = postFile.FileName;

                // Tạo đường dẫn vật lý cho việc lưu trữ tệp tin trên máy chủ
                var physicalPath = _webHostEnvironment.ContentRootPath + "/Photos/" + fileName;

                // Mở một luồng để sao chép dữ liệu từ tệp tin gửi đến vào tệp tin trên máy chủ
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postFile.CopyTo(stream);
                }

                // Trả về tên của tệp tin đã được lưu trữ thành công
                return new JsonResult(fileName);
            }
            catch (Exception)
            {
                // Nếu có lỗi xảy ra, trả về tên của tệp tin mặc định
                return new JsonResult("img-default.jpg");
            }
        }
    }
}

/*
1. [HttpDelete]: Đây là một attribute được sử dụng trong ASP.NET Core để chỉ định rằng phương thức này được gọi khi có một yêu cầu HTTP DELETE được gửi đến đường dẫn tương ứng.

2. public JsonResult Delete(Dish Dish): Đây là khai báo của phương thức Delete. Nó nhận một đối tượng kiểu Dish như một tham số, giả định rằng thông tin về Dish cần xóa được truyền từ client.

3. string query = @"Delete from Dishs" + $"Where DishId = {Dish.DishId}";: Đây là câu lệnh SQL để xóa một bản ghi từ bảng Dishs. Biến Dish.DishId được sử dụng để chỉ định bản ghi cụ thể cần xóa.

4. DataTable table = new DataTable();: Tạo một đối tượng DataTable để lưu trữ dữ liệu từ kết quả của câu lệnh SQL.

5. String sqlDataSource = _configuration.GetConnectionString("RestaurantManager");: Lấy chuỗi kết nối đến cơ sở dữ liệu từ cấu hình của ứng dụng. Trong trường hợp này, nó đang lấy chuỗi kết nối có tên "RestaurantManager".

6. SqlDataReader myReader;: Khai báo một đối tượng SqlDataReader để đọc dữ liệu từ câu lệnh SQL.

7. using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource)): Tạo một kết nối mới đến cơ sở dữ liệu sử dụng chuỗi kết nối sqlDataSource. Sử dụng using đảm bảo rằng tài nguyên được giải phóng sau khi hoàn thành công việc của khối using.

8. sqlConnection.Open();: Mở kết nối đến cơ sở dữ liệu.

9. using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection)): Tạo một đối tượng SqlCommand để thực thi câu lệnh SQL query trên kết nối sqlConnection. Cũng giống như SqlConnection, việc sử dụng using đảm bảo rằng tài nguyên được giải phóng sau khi hoàn thành công việc của khối using.

10. myReader = sqlCommand.ExecuteReader();: Thực thi câu lệnh SQL và gán kết quả vào myReader.

11. table.Load(myReader);: Load dữ liệu từ myReader vào DataTable table.

12. myReader.Close();: Đóng SqlDataReader sau khi hoàn thành việc đọc dữ liệu.

13. sqlConnection.Close();: Đóng kết nối đến cơ sở dữ liệu sau khi hoàn thành việc thực hiện câu lệnh SQL.

14. return new JsonResult("Xóa thành công.");: Trả về một kết quả JsonResult thông báo rằng việc xóa đã thành công.
 */