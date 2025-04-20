INSERT INTO Categories(Id, CategoryName, Description, IsActive, CreatedAt, UpdatedAt)
VALUES 
(NEWID(), N'Trà Sữa', N'Các loại trà sữa thơm ngon', 1, GETDATE(), GETDATE()),
(NEWID(), N'Topping', N'Topping kèm thêm cho trà sữa', 1, GETDATE(), GETDATE());


-- Trà sữa
INSERT INTO Products(Id, ProductName, Description, CategoryId, Price, CreatedAt, IsActive)
VALUES 
(NEWID(), N'Trà Sữa Truyền Thống', N'Vị truyền thống đậm đà', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 30000, GETDATE(), 1),
(NEWID(), N'Trà Sữa Trân Châu Đen', N'Kèm trân châu đen dẻo thơm', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 35000, GETDATE(), 1),
(NEWID(), N'Trà Sữa Socola', N'Vị socola thơm béo', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 40000, GETDATE(), 1),
(NEWID(), N'Trà Sữa Matcha', N'Matcha Nhật Bản', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 42000, GETDATE(),1),
(NEWID(), N'Trà Sữa Khoai Môn', N'Khoai môn thơm lừng', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 38000, GETDATE(),1),
(NEWID(), N'Trà Sữa Dâu', N'Dâu tươi ngọt ngào', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 37000, GETDATE(),1),
(NEWID(), N'Trà Sữa Bạc Hà', N'Mát lạnh sảng khoái', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 36000, GETDATE(),1),
(NEWID(), N'Trà Sữa Phô Mai', N'Kem phô mai béo ngậy', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 45000, GETDATE(),1),
(NEWID(), N'Trà Sữa Oolong', N'Hương Oolong thanh dịu', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 39000, GETDATE(),1),
(NEWID(), N'Trà Sữa Hồng Trà', N'Vị trà đậm đà', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 34000, GETDATE(),1);

-- Topping
INSERT INTO Products(Id, ProductName, Description, CategoryId, Price, CreatedAt, IsActive)
VALUES 
(NEWID(), N'Trân Châu Đen', N'Trân châu dẻo, ngọt vừa', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 5000, GETDATE(),1),
(NEWID(), N'Trân Châu Trắng', N'Mềm, dai, nhẹ vị', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 5000, GETDATE(),1),
(NEWID(), N'Thạch Cà Phê', N'Thạch thơm mùi cà phê', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 6000,  GETDATE(),1),
(NEWID(), N'Thạch Dừa', N'Thạch dừa giòn', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 6000,  GETDATE(),1),
(NEWID(), N'Pudding Trứng', N'Pudding thơm, béo', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 7000,  GETDATE(),1),
(NEWID(), N'Thạch Phô Mai', N'Thạch dai, béo phô mai', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 8000,  GETDATE(),1),
(NEWID(), N'Hạt Thủy Tinh', N'Giòn giòn, ngọt nhẹ', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 5000, GETDATE(),1),
(NEWID(), N'Trân Châu Sợi', N'Lạ miệng, dẻo dai', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 6000,  GETDATE(),1),
(NEWID(), N'Pudding Socola', N'Pudding vị cacao', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 7000,  GETDATE(),1),
(NEWID(), N'Phô Mai Tươi', N'Miếng phô mai tươi', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 10000,  GETDATE(),1);

INSERT INTO Users(Id, Username, PasswordHash, PhoneNumber, Role, IsActive, CreatedAt)
VALUES
(NEWID(), N'admin1','123','0123456789',1,1,GETDATE()),
(NEWID(), N'admin2','123','0123456789',1,1,GETDATE()),
(NEWID(), N'manager1','123','0123456789',2,1,GETDATE()),
(NEWID(), N'manager2','123','0123456789',2,1,GETDATE()),
(NEWID(), N'staff1','123','0123456789',3,1,GETDATE()),
(NEWID(), N'staff2','123','0123456789',3,1,GETDATE()),
(NEWID(), N'customer1','123','0123456789',4,1,GETDATE()),
(NEWID(), N'customer2','123','0123456789',4,1,GETDATE());

