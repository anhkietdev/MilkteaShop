INSERT INTO Categories(Id, CategoryName, Description, IsActive, CreatedAt, UpdatedAt)
VALUES 
(NEWID(), N'Trà Sữa', N'Các loại trà sữa thơm ngon', 1, GETDATE(), GETDATE()),
(NEWID(), N'Topping', N'Topping kèm thêm cho trà sữa', 1, GETDATE(), GETDATE());


-- Trà sữa
INSERT INTO Products(Id, ProductName, Description, CategoryId, Price, Size, ImageUrl)
VALUES 
(NEWID(), N'Trà Sữa Truyền Thống', N'Vị truyền thống đậm đà', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 30000, 1, '/images/milk-tea/classic.jpg'),
(NEWID(), N'Trà Sữa Trân Châu Đen', N'Kèm trân châu đen dẻo thơm', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 35000, 1, '/images/milk-tea/boba.jpg'),
(NEWID(), N'Trà Sữa Socola', N'Vị socola thơm béo', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 40000, 1, '/images/milk-tea/chocolate.jpg'),
(NEWID(), N'Trà Sữa Matcha', N'Matcha Nhật Bản', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 42000, 1, '/images/milk-tea/matcha.jpg'),
(NEWID(), N'Trà Sữa Khoai Môn', N'Khoai môn thơm lừng', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 38000, 1, '/images/milk-tea/taro.jpg'),
(NEWID(), N'Trà Sữa Dâu', N'Dâu tươi ngọt ngào', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 37000, 1, '/images/milk-tea/strawberry.jpg'),
(NEWID(), N'Trà Sữa Bạc Hà', N'Mát lạnh sảng khoái', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 36000, 1, '/images/milk-tea/mint.jpg'),
(NEWID(), N'Trà Sữa Phô Mai', N'Kem phô mai béo ngậy', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 45000, 1, '/images/milk-tea/cheese.jpg'),
(NEWID(), N'Trà Sữa Oolong', N'Hương Oolong thanh dịu', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 39000, 1, '/images/milk-tea/oolong.jpg'),
(NEWID(), N'Trà Sữa Hồng Trà', N'Vị trà đậm đà', '9BB11DD2-BE35-474D-9B2F-3D4587C59594', 34000, 1, '/images/milk-tea/redtea.jpg');

-- Topping
INSERT INTO Products(Id, ProductName, Description, CategoryId, Price, Size, ImageUrl)
VALUES 
(NEWID(), N'Trân Châu Đen', N'Trân châu dẻo, ngọt vừa', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 5000, 0, '/images/topping/boba.jpg'),
(NEWID(), N'Trân Châu Trắng', N'Mềm, dai, nhẹ vị', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 5000, 0, '/images/topping/whiteboba.jpg'),
(NEWID(), N'Thạch Cà Phê', N'Thạch thơm mùi cà phê', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 6000, 0, '/images/topping/coffeejelly.jpg'),
(NEWID(), N'Thạch Dừa', N'Thạch dừa giòn', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 6000, 0, '/images/topping/coconuts.jpg'),
(NEWID(), N'Pudding Trứng', N'Pudding thơm, béo', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 7000, 0, '/images/topping/pudding.jpg'),
(NEWID(), N'Thạch Phô Mai', N'Thạch dai, béo phô mai', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 8000, 0, '/images/topping/cheesejelly.jpg'),
(NEWID(), N'Hạt Thủy Tinh', N'Giòn giòn, ngọt nhẹ', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 5000, 0, '/images/topping/popping.jpg'),
(NEWID(), N'Trân Châu Sợi', N'Lạ miệng, dẻo dai', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 6000, 0, '/images/topping/noodleboba.jpg'),
(NEWID(), N'Pudding Socola', N'Pudding vị cacao', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 7000, 0, '/images/topping/chocopudding.jpg'),
(NEWID(), N'Phô Mai Tươi', N'Miếng phô mai tươi', '11AF5B3C-8817-4A18-B7E3-84BF8F1BBA45', 10000, 0, '/images/topping/freshcheese.jpg');
