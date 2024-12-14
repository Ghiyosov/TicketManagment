-- Создание таблицы Tickets
CREATE TABLE Tickets (
    TicketId SERIAL PRIMARY KEY,
    Type VARCHAR(50) NOT NULL,
    Title VARCHAR(150) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    EventDateTime TIMESTAMP NOT NULL
);

-- Создание таблицы Customers
CREATE TABLE Customers (
    CustomerId SERIAL PRIMARY KEY,
    FullName VARCHAR(150) NOT NULL,
    Email VARCHAR(150) UNIQUE NOT NULL,
    Phone VARCHAR(20) UNIQUE NOT NULL
);

-- Создание таблицы Purchases
CREATE TABLE Purchases (
    PurchaseId SERIAL PRIMARY KEY,
    TicketId INT NOT NULL,
    CustomerId INT NOT NULL,
    PurchaseDateTime TIMESTAMP NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    TotalPrice DECIMAL(10, 2) NOT NULL CHECK (TotalPrice >= 0),
    CONSTRAINT fk_ticket FOREIGN KEY (TicketId) REFERENCES Tickets (TicketId) ON DELETE CASCADE,
    CONSTRAINT fk_customer FOREIGN KEY (CustomerId) REFERENCES Customers (CustomerId) ON DELETE CASCADE
);

-- Создание таблицы Locations
CREATE TABLE Locations (
    LocationId SERIAL PRIMARY KEY,
    City VARCHAR(100) NOT NULL,
    Address VARCHAR(200) NOT NULL,
    LocationType VARCHAR(50) NOT NULL
);

-- Создание таблицы TicketLocations
CREATE TABLE TicketLocations (
    TicketId INT NOT NULL,
    LocationId INT NOT NULL,
    CONSTRAINT pk_ticketlocations PRIMARY KEY (TicketId, LocationId),
    CONSTRAINT fk_ticket FOREIGN KEY (TicketId) REFERENCES Tickets (TicketId) ON DELETE CASCADE,
    CONSTRAINT fk_location FOREIGN KEY (LocationId) REFERENCES Locations (LocationId) ON DELETE CASCADE
);


-- Примеры данных для таблицы Tickets
INSERT INTO Tickets (Type, Title, Price, EventDateTime) VALUES
('Flight', 'Рейс Москва - Нью-Йорк', 25000.00, '2024-12-20 15:30:00'),
('Train', 'Поезд Душанбе - Худжанд', 150.00, '2024-12-21 08:00:00'),
('Bus', 'Автобус Душанбе - Вахдат', 50.00, '2024-12-22 09:00:00'),
('Event', 'Концерт в Парке Победы', 500.00, '2024-12-25 18:00:00'),
('Flight', 'Рейс Душанбе - Алматы', 20000.00, '2024-12-23 14:00:00');

-- Примеры данных для таблицы Customers
INSERT INTO Customers (FullName, Email, Phone) VALUES
('Иван Иванов', 'ivanov@example.com', '+71234567890'),
('Алия Сафарова', 'aliya@example.com', '+992908765432'),
('Дмитрий Петров', 'dmitry@example.com', '+79654321098'),
('Саид Алиев', 'said@example.com', '+992933223344'),
('Мария Павлова', 'maria@example.com', '+79211234567');

-- Примеры данных для таблицы Purchases
INSERT INTO Purchases (TicketId, CustomerId, PurchaseDateTime, Quantity, TotalPrice) VALUES
(1, 1, '2024-12-10 10:00:00', 1, 25000.00),
(2, 2, '2024-12-11 11:30:00', 2, 300.00),
(3, 3, '2024-12-12 12:45:00', 1, 50.00),
(4, 4, '2024-12-13 13:00:00', 3, 1500.00),
(5, 5, '2024-12-14 14:30:00', 1, 20000.00);

-- Примеры данных для таблицы Locations
INSERT INTO Locations (City, Address, LocationType) VALUES
('Москва', 'Шереметьево, Терминал D', 'Airport'),
('Душанбе', 'Ж/Д вокзал', 'Railway Station'),
('Худжанд', 'Центральный Автовокзал', 'Bus Station'),
('Душанбе', 'Парк Победы', 'Event Hall'),
('Алматы', 'Международный аэропорт Алматы', 'Airport');

-- Примеры данных для таблицы TicketLocations
INSERT INTO TicketLocations (TicketId, LocationId) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);
INSERT INTO TicketLocations (TicketId, LocationId) VALUES
(2, 1),
(3, 1);




select  from Tickets as t
join TicketLocations as tl on t.TicketId=tl.TicketId
where tl.LocationId=1;