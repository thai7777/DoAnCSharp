# DoAnCSharp
## đồ án .net winform application cnpm4
cách restore database

 ** Khi bạn muốn khôi phục một bản sao lưu từ một máy tính sang máy tính khác, bạn cần thực hiện các bước sau: **

- Sử Dụng SQL Server Management Studio (SSMS):
  - Copy Tập Tin Sao Lưu:
     Chuyển tập tin sao lưu (ví dụ: BackupTenCoSoDuLieu.bak) từ máy tính cũ sang máy tính mới.
  - Mở SQL Server Management Studio (SSMS) trên Máy Tính Mới:

   - Mở SSMS và kết nối với SQL Server trên máy tính mới.
Chọn Cơ Sở Dữ Liệu Mới:

Nếu cơ sở dữ liệu chưa tồn tại, tạo mới một cơ sở dữ liệu trên máy tính mới.

Restore Bằng SSMS:

Chuột phải vào cơ sở dữ liệu mới.
Chọn "Tasks" > "Restore" > "Database".
Chọn "Device" và chọn tập tin sao lưu đã chép.
Thiết lập các tùy chọn khác nếu cần thiết.
Nhấn "OK" để khôi phục cơ sở dữ liệu.
Sử Dụng Câu Lệnh SQL RESTORE:
Copy Tập Tin Sao Lưu:

Chuyển tập tin sao lưu (ví dụ: BackupTenCoSoDuLieu.bak) từ máy tính cũ sang máy tính mới.
Mở SSMS trên Máy Tính Mới và Kết Nối Đến SQL Server:

Mở SSMS và kết nối với SQL Server trên máy tính mới.
Chọn Cơ Sở Dữ Liệu Mới (nếu chưa tồn tại):

Tạo mới cơ sở dữ liệu trên máy tính mới nếu chưa tồn tại.
Thực Hiện Câu Lệnh SQL RESTORE Trên Máy Tính Mới:

Sử dụng câu lệnh SQL RESTORE để khôi phục cơ sở dữ liệu.

`RESTORE DATABASE TenCoSoDuLieu FROM DISK = 'C:\DuongDan\SaoLuu\BackupTenCoSoDuLieu.bak';`
Thay thế TenCoSoDuLieu bằng tên cơ sở dữ liệu mới và C:\DuongDan\SaoLuu\BackupTenCoSoDuLieu.bak với đường dẫn đến tập tin sao lưu.
Lưu ý rằng quyền RESTORE DATABASE là cần thiết để thực hiện câu lệnh này.

Lưu Ý Quan Trọng:
Đảm bảo rằng bạn có đủ quyền và cấp phép để thực hiện thao tác restore trên máy tính mới.
Luôn giữ tập tin sao lưu ở một vị trí an toàn và bảo mật.
Kiểm tra và đảm bảo rằng phiên bản SQL Server trên cả hai máy tính là tương đương để tránh vấn đề không tương thích.
