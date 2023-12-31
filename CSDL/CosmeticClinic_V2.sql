﻿Create Database QL_CosmeticClinic_V2
go
use QL_CosmeticClinic_V2

CREATE TABLE Branch (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    "Name" NVARCHAR(255),
    "Address" NVARCHAR(255),
    Phone NVARCHAR(11),
    Email NVARCHAR(255),
	IsDelete bit,
	DateDelete nvarchar(20),
);
CREATE TABLE UserType (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    "Name" NVARCHAR(100),
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20)
);
CREATE TABLE UserStatus (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    StatusName NVARCHAR(100),
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20)
);
CREATE TABLE "User" (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    IDCard NVARCHAR(20),
    FullName NVARCHAR(255),
    Birthdate NVARCHAR(255),
    Gender NVARCHAR(20),
    Hometown NVARCHAR(255),
    Phone NVARCHAR(20),
    Email NVARCHAR(255),
    BankAccount NVARCHAR(20),
    CurrentAddress NVARCHAR(255),
    HealthInsurance NVARCHAR(20),
    SocialInsurance NVARCHAR(20),
    Salary INT,
    UserTypeID INT,
    UserStatusID INT,
    TimesPregnant INT,
    BranchID INT,
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (UserTypeID) REFERENCES UserType(ID),
    FOREIGN KEY (UserStatusID) REFERENCES UserStatus(ID),
    FOREIGN KEY (BranchID) REFERENCES Branch(ID)
);
CREATE TABLE AccountType (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    AccountTypeName NVARCHAR(255),
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20)
);
CREATE TABLE UserAccount (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(255),
    "Password" NVARCHAR(255),
    Email NVARCHAR(255),
    UserStatusID INT,
    UserID INT,
    AccountTypeID INT,
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (UserStatusID) REFERENCES UserStatus(ID),
    FOREIGN KEY (UserID) REFERENCES "User"(ID),
    FOREIGN KEY (AccountTypeID) REFERENCES AccountType(ID)
);



CREATE TABLE ActionType (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(255),
    IsDelete bit,
	DateDelete nvarchar(20)
);
CREATE TABLE "Action" (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    "Time" NVARCHAR(40),
    ContentOfChange NVARCHAR(1000),
    ActionTypeID INT,
    UserID INT,
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (ActionTypeID) REFERENCES ActionType(ID),
    FOREIGN KEY (UserID) REFERENCES UserAccount(ID)
);
CREATE TABLE CustomerType (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(255),
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20)
);
CREATE TABLE CustomerStatus (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    StatusName NVARCHAR(255),
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20)
);
CREATE TABLE Customer (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(255),
    Birthdate NVARCHAR(20),
    PhoneNumber NVARCHAR(255),
    IDCard NVARCHAR(255),
    FacebookLink NVARCHAR(255),
    ZaloLink NVARCHAR(255),
    Email NVARCHAR(255),
    Gender NVARCHAR(20),
    "Address" NVARCHAR(255),
    InterestedServices NVARCHAR(1000),
    CustomerStatusID INT,
    CustomerTypeID INT,
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20)
    FOREIGN KEY (CustomerStatusID) REFERENCES CustomerStatus(ID),
    FOREIGN KEY (CustomerTypeID) REFERENCES CustomerType(ID)
);
-- Thêm cột CustomerId vào bảng UserAccount
ALTER TABLE UserAccount
ADD CustomerId INT;

-- Thêm ràng buộc khóa ngoại cho cột CustomerId
ALTER TABLE UserAccount
ADD CONSTRAINT FK_UserAccount_Customer
FOREIGN KEY (CustomerId) REFERENCES Customer(ID);

CREATE TABLE ServiceType (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(255),
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20)
);
CREATE TABLE Promotion (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    PromotionName NVARCHAR(255),
    PromotionValue NVARCHAR(255),
    PromotionTime NVARCHAR(20),
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20)
);
CREATE TABLE "Service" (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ServiceName NVARCHAR(255),
    ServiceTypeID INT,
    ServiceDetails NVARCHAR(2000),
    Price INT,
    Other NVARCHAR(1000),
    FollowUpDays INT,
    AppliedPromotionID INT,
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (ServiceTypeID) REFERENCES ServiceType(ID),
    FOREIGN KEY (AppliedPromotionID) REFERENCES Promotion(ID)
);
CREATE TABLE ProductType (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ProductTypeName NVARCHAR(255),
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20)
);
CREATE TABLE Product (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(255),
    Introduction NVARCHAR(1500),
    Functionality NVARCHAR(1500),
    Origin NVARCHAR(255),
    SellingPrice INT,
    PurchasePrice INT,
    AppliedPromotionID INT,
    ProductTypeID INT,
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (AppliedPromotionID) REFERENCES Promotion(ID),
    FOREIGN KEY (ProductTypeID) REFERENCES ProductType(ID)
);
CREATE TABLE Product_Branch (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT,
    BranchID INT,
    Quantity INT,
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (ProductID) REFERENCES Product(ID),
    FOREIGN KEY (BranchID) REFERENCES Branch(ID)
);
CREATE TABLE WarrantyType (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    WarrantyTypeName NVARCHAR(255),
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20)
);
CREATE TABLE WarrantyReceipt (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    WarrantyDate NVARCHAR(20),
    WarrantyHandledByUserID INT,
    FollowUpUserID INT,
    WarrantyEndDate NVARCHAR(20),
    CustomerID INT,
    ServicedServiceID INT,
    WarrantyDetails NVARCHAR(1500),
    HandledAtBranchID INT,
    WarrantyTypeID INT,
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (WarrantyHandledByUserID) REFERENCES "User"(ID),
    FOREIGN KEY (FollowUpUserID) REFERENCES "User"(ID),
    FOREIGN KEY (CustomerID) REFERENCES Customer(ID),
    FOREIGN KEY (ServicedServiceID) REFERENCES "Service"(ID),
    FOREIGN KEY (WarrantyTypeID) REFERENCES WarrantyType(ID),
    FOREIGN KEY (HandledAtBranchID) REFERENCES Branch(ID)
);
CREATE TABLE FollowUpStatus (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    StatusName NVARCHAR(255),
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20)
);
CREATE TABLE CustomerFollowUp (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT,
    UserID INT,
    FollowUpStatusID INT,
    FollowUpBranchID INT,
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (CustomerID) REFERENCES Customer(ID),
    FOREIGN KEY (UserID) REFERENCES "User"(ID),
    FOREIGN KEY (FollowUpStatusID) REFERENCES FollowUpStatus(ID),
    FOREIGN KEY (FollowUpBranchID) REFERENCES Branch(ID)
);
CREATE TABLE FollowUpDetails (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    FollowUpReceiptID INT,
    Details NVARCHAR(1000),
    FollowUpDate NVARCHAR(20),
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (FollowUpReceiptID) REFERENCES CustomerFollowUp(ID)
);
CREATE TABLE AppointmentType (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(255),
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20)
);
CREATE TABLE AppointmentStatus (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    StatusName NVARCHAR(255),
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20)
);
CREATE TABLE Appointment (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    CreatedByUserID INT,
    ReceivedByUserID INT,
    CreationDate NVARCHAR(20),
    AppointmentDate NVARCHAR(20),
    CustomerID INT,
    ServicePerformedID INT,
    BranchID INT,
    AppointmentStatusID INT,
    AppointmentTypeID INT,
    Other NVARCHAR(255),
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (CreatedByUserID) REFERENCES "User"(ID),
    FOREIGN KEY (ReceivedByUserID) REFERENCES "User"(ID),
    FOREIGN KEY (CustomerID) REFERENCES Customer(ID),
    FOREIGN KEY (ServicePerformedID) REFERENCES Service(ID),
    FOREIGN KEY (BranchID) REFERENCES Branch(ID),
    FOREIGN KEY (AppointmentTypeID) REFERENCES AppointmentType(ID),
    FOREIGN KEY (AppointmentStatusID) REFERENCES AppointmentStatus(ID)
);
CREATE TABLE ShoppingCart (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    UserAccountID INT,
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (UserAccountID) REFERENCES UserAccount(ID)
);
CREATE TABLE ShoppingCartItem (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ShoppingCartID INT,
    ProductID INT,
    ServiceID INT,
    Quantity INT,
    Price INT,
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (ShoppingCartID) REFERENCES ShoppingCart(ID),
    FOREIGN KEY (ProductID) REFERENCES Product(ID),
    FOREIGN KEY (ServiceID) REFERENCES "Service"(ID)
);
CREATE TABLE PostType (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(255),
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20)
);
CREATE TABLE Post (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255),
	Introduce NVARCHAR(2000),
    PostingDateCreate NVARCHAR(20),
	PostingDateUpdate NVARCHAR(20),
	Content NVARCHAR(max),
	"Image" NVARCHAR(max),
    ViewsCount INT,
    PostTypeID INT,
    PostedByUserID INT,
    IsDelete bit,
	DateDelete nvarchar(20)
    FOREIGN KEY (PostTypeID) REFERENCES PostType(ID),
    FOREIGN KEY (PostedByUserID) REFERENCES UserAccount(ID)
);
CREATE TABLE ProductImage (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT,
    "Image" NVARCHAR(max),
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (ProductID) REFERENCES Product(ID)
);
CREATE TABLE CustomerImage (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT,
    "Image" NVARCHAR(max),
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (CustomerID) REFERENCES Customer(ID)
);
CREATE TABLE OrderStatus (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    StatusName NVARCHAR(255),
    IsDelete bit,
	DateDelete nvarchar(20)
);
CREATE TABLE "Order" (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    OrderStatusID INT,
    OrderDate NVARCHAR(20),
    BranchID INT,
    CustomerID INT,
    TotalAmount INT,
    DeliveryAddress NVARCHAR(255),
    RecipientPhoneNumber NVARCHAR(20),
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (OrderStatusID) REFERENCES OrderStatus(ID),
    FOREIGN KEY (BranchID) REFERENCES Branch(ID),
    FOREIGN KEY (CustomerID) REFERENCES Customer(ID)
);
CREATE TABLE OrderDetails (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    SeqNumber INT,
    OrderID INT,
    ProductID INT,
    Quantity INT,
    PromotionID INT,
    TotalPrice INT,
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (OrderID) REFERENCES "Order"(ID),
    FOREIGN KEY (ProductID) REFERENCES Product(ID),
    FOREIGN KEY (PromotionID) REFERENCES Promotion(ID)
);
CREATE TABLE InvoiceType (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceTypeName NVARCHAR(255),
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20)
);
CREATE TABLE Invoice (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceTypeID INT,
    InvoiceDate NVARCHAR(20),
    CreatedByUserID INT,
    CustomerID INT,
    PaymentMethod NVARCHAR(255),
    TotalAmount INT,
    BranchID INT,
    IsDelete bit,
	DateDelete nvarchar(20),
    Other NVARCHAR(1000),
    FOREIGN KEY (InvoiceTypeID) REFERENCES InvoiceType(ID),
    FOREIGN KEY (CreatedByUserID) REFERENCES "User"(ID),
    FOREIGN KEY (CustomerID) REFERENCES Customer(ID),
    FOREIGN KEY (BranchID) REFERENCES Branch(ID)
);
CREATE TABLE InvoiceDetails (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    SeqNumber INT,
    InvoiceID INT,
    Content NVARCHAR(255),
    Quantity INT,
	Price INT,
    Discount INT,
    TotalPrice Float,
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (InvoiceID) REFERENCES Invoice(ID),
    FOREIGN KEY (Discount) REFERENCES Promotion(ID)
);
CREATE TABLE PaymentStatus (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    StatusName NVARCHAR(255),
    IsDelete bit,
	DateDelete nvarchar(20)
);
CREATE TABLE OnlinePayment (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceID INT,
    PaymentDescription NVARCHAR(255),
    PaymentStatusID INT,
    PaymentDate NVARCHAR(20),
    AmountPaid INT,
    BranchID INT,
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (InvoiceID) REFERENCES Invoice(ID),
    FOREIGN KEY (PaymentStatusID) REFERENCES PaymentStatus(ID),
    FOREIGN KEY (BranchID) REFERENCES Branch(ID)
);
CREATE TABLE CustomerHistory (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    UsageDate NVARCHAR(20),
    ServiceID INT,
    TotalAmount INT,
    WarrantyID INT,
    InvoiceID INT,
    ConsultingUserID INT,
    CustomerCareUserID INT,
    TechnicalUserID INT,
    ServiceBranchID INT,
    Other NVARCHAR(1000),
    IsDelete bit,
	DateDelete nvarchar(20),
    FOREIGN KEY (ServiceID) REFERENCES Service(ID),
    FOREIGN KEY (WarrantyID) REFERENCES WarrantyReceipt(ID),
    FOREIGN KEY (InvoiceID) REFERENCES Invoice(ID),
    FOREIGN KEY (ConsultingUserID) REFERENCES "User"(ID),
    FOREIGN KEY (CustomerCareUserID) REFERENCES "User"(ID),
    FOREIGN KEY (TechnicalUserID) REFERENCES "User"(ID),
    FOREIGN KEY (ServiceBranchID) REFERENCES Branch(ID)
);


INSERT INTO AccountType (AccountTypeName, Other, IsDelete, DateDelete)
VALUES
    (N'Admin', N'Quản lý toàn hộ hệ thống', 0, NULL),
	(N'Quản lý', N'Là người dùng có thể quản lý một số phần quan trong của hệ thống', 0, NULL),
	(N'Nhân Viên', N'Là người dùng thông thường thường', 0, NULL),
	(N'Khách hàng', N'Là người dùng có thể quản lý một số phần quan trong của hệ thống', 0, NULL)

INSERT INTO UserStatus (StatusName, Other, IsDelete, DateDelete)
VALUES
    (N'Hoạt động', N'Thông tin bổ sung cho trạng thái hoạt động', 0, NULL),
    (N'Vô hiệu', N'Thông tin bổ sung cho trạng thái vô hiệu', 0, NULL),
    (N'Chờ xác nhận', N'Thông tin bổ sung cho trạng thái chờ xác nhận', 0, NULL),
    (N'Tạm dừng', N'Thông tin bổ sung cho trạng thái tạm dừng', 0, NULL);

INSERT INTO InvoiceType (InvoiceTypeName, Other, IsDelete, DateDelete) VALUES
    ('Hóa Đơn Dịch Vụ', 'cập nhật sau', NULL, NULL),
    ('Hóa Đơn Bán Hàng', 'chờ cập nhật', NULL, NULL);

INSERT INTO ActionType (TypeName, IsDelete, DateDelete)
VALUES
    (N'Thêm', 0, NULL),
    (N'Sửa', 0, NULL),
    (N'Xóa', 0, NULL),
    (N'Khác', 0, NULL);

	INSERT INTO OrderStatus (StatusName, IsDelete, DateDelete) VALUES
    ('Chờ xác nhận', 0, NULL),
    ('Chờ lấy hàng', 0, NULL),
    ('Đang vẫn chuyển', 0, NULL),
    ('Đã hủy', 0, NULL),
    ('Hoàn thành', 0, NULL);


INSERT INTO UserType ("Name", Other, IsDelete, DateDelete)
VALUES (N'Quản lý', N'Chờ cập nhật', 0, NULL),
       (N'Bác sĩ', N'Chờ cập nhật', 0, NULL),
       (N'Kỹ thuật', N'Chờ cập nhật', 0, NULL),
	   (N'Nhân viên tư vấn', N'Chờ cập nhật', 0, NULL),
	   (N'Nhân viên sale', N'Chờ cập nhật', 0, NULL);

-- Thêm dữ liệu mẫu vào bảng User
INSERT INTO "User" (IDCard, FullName, Birthdate, Gender, Hometown, Phone, Email, BankAccount, CurrentAddress, HealthInsurance, SocialInsurance, Salary, UserTypeID, UserStatusID, TimesPregnant, BranchID, Other, IsDelete, DateDelete)
VALUES
    ('0', 'Admin', '0', '2', N'Chờ cập nhật', '0', N'Chờ cập nhật', '0', N'Chờ cập nhật', N'Chờ cập nhật', N'Chờ cập nhật', 0, 1, 1, NULL, NULL, N'Chờ cập nhật', 0, NULL);

INSERT INTO UserAccount (Username, "Password", Email, UserStatusID, UserID, AccountTypeID, IsDelete, DateDelete)
VALUES
    (N'Admin', N'123', N'user1@email.com', 1, 1 , 1, 0, '');

INSERT INTO PostType (TypeName, Other, IsDelete, DateDelete)
VALUES (N'Dịch Vụ Nổi Bật', N'Được tạo bởi hệ thống, Không khuyến khích bạn thay đổi nội dung này để tránh ảnh hưởng đến hệ thống', 0, NULL),
(N'Bài viết', N'Được tạo bởi hệ thống, Không khuyến khích bạn thay đổi nội dung này để tránh ảnh hưởng đến hệ thống', 0, NULL);

INSERT INTO Post (Title, Introduce, PostingDateCreate, PostingDateUpdate, Content, "Image", ViewsCount, PostTypeID, PostedByUserID, IsDelete, DateDelete)
VALUES (N'#1 Hà Tiểu Vy 10 Năm Trong Ngành Thẩm Mỹ', N'Một chuyên gia SEO tài năng và có uy tín, đang tỏa sáng trong ngành thẩm mỹ với 10 năm kinh nghiệm đầy đủ. Với khả năng hiểu biết sâu rộng về cả lĩnh vực thẩm mỹ và chiến lược tối ưu hóa công cụ tìm kiếm, cô ấy đã đóng góp đáng kể vào sự thành công của chuỗi thẩm mỹ viện. Không chỉ giỏi về kỹ thuật, Hà Tiểu Vi còn được biết đến với khả năng sáng tạo và linh hoạt trong việc áp dụng các chiến lược SEO tiên tiến nhất. Với niềm đam mê và cam kết cao, cô ấy luôn nỗ lực không ngừng để đảm bảo rằng thương hiệu mà mình đại diện luôn xuất hiện ở vị trí cao nhất trên các trang kết quả tìm kiếm.',
'2023-01-01', '2023-01-02', N'', N'image_url.jpg', 0, 1, NULL, NULL, NULL);

-- Thêm dữ liệu mẫu vào bảng Customer
INSERT INTO Customer (FullName, Birthdate, PhoneNumber, IDCard, FacebookLink, ZaloLink, Email, Gender, "Address", InterestedServices, CustomerStatusID, CustomerTypeID, Other, IsDelete, DateDelete) VALUES
    ('Lý Tự Hào', '30/02/2002', '0342757202', '123456789', 'chờ cập nhật', 'chờ cập nhật', 'lytuhao2002@gmail.com', '1', 'Bình Thủy, Cần Thơ', 'Chờ cập nhật', 1, 1, 'Chờ cập nhật', 0, NULL),
    ('Hà Thanh Bền', '30/02/2002', '0963462310', '234567890', 'chờ cập nhật', 'chờ cập nhật', 'hathanhben2002@gmail.com', '0', 'Bình Thủy, Cần Thơ', 'Chờ cập nhật', 1, 2, 'Chờ cập nhật', 0, NULL);


INSERT INTO Invoice (InvoiceTypeID, InvoiceDate, CreatedByUserID, CustomerID, PaymentMethod, TotalAmount, BranchID, IsDelete, DateDelete, Other) VALUES
    (1, '13:27:01 12/12/2023', 1, 2, N'Tiền mặt', 12345, NULL, 0, NULL, 'Đã hoàn thành'),
    (1, '13:27:01 12/12/2023', 1, 2, N'Tiền mặt', 12345, NULL, 0, NULL, 'Đã hoàn thành');
