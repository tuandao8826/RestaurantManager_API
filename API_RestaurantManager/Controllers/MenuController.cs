using API_RestaurantManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_RestaurantManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MenuController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Select * from Menus";

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
        public JsonResult Post(Menu menu)
        {
            string query = @"Insert into Menus(Name) values" +
                $"(" +
                $"N'{menu.Name}'" +
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
        public JsonResult Put(Menu menu)
        {
            string query = @"Update Menus" +
                $" Set Name = N'{menu.Name}'" +
                $" Where MenuId = {menu.MenuId}";

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

        [HttpDelete("{menuId}")]
        public JsonResult Delete(int menuId)
        {
            string query = @"Delete from Menus" +
                $" Where MenuId = {menuId}";

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
    }
}

/*
1. [HttpDelete]: Đây là một attribute được sử dụng trong ASP.NET Core để chỉ định rằng phương thức này được gọi khi có một yêu cầu HTTP DELETE được gửi đến đường dẫn tương ứng.

2. public JsonResult Delete(Menu menu): Đây là khai báo của phương thức Delete. Nó nhận một đối tượng kiểu Menu như một tham số, giả định rằng thông tin về Menu cần xóa được truyền từ client.

3. string query = @"Delete from Menus" + $"Where MenuId = {menu.MenuId}";: Đây là câu lệnh SQL để xóa một bản ghi từ bảng Menus. Biến menu.MenuId được sử dụng để chỉ định bản ghi cụ thể cần xóa.

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