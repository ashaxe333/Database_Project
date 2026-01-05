create database library;
use library;

SET autocommit = 1;

-- TABLES
CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE authors (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

CREATE TABLE books (
    id INT AUTO_INCREMENT PRIMARY KEY,
    title VARCHAR(200) NOT NULL,
    published_year INT,
    available BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE book_authors (
    id INT AUTO_INCREMENT PRIMARY KEY,
    book_id INT NOT NULL,
    author_id INT NOT NULL,
    FOREIGN KEY (book_id) REFERENCES books(id),
    FOREIGN KEY (author_id) REFERENCES authors(id),
    UNIQUE (book_id, author_id)
);

CREATE TABLE loans (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    book_id INT NOT NULL,
    loan_date DATE NOT NULL,
    return_date DATE,
    status ENUM('BORROWED', 'RETURNED', 'OVERDUE') NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (book_id) REFERENCES books(id)
);

CREATE TABLE payments (
    id INT AUTO_INCREMENT PRIMARY KEY,
    loan_id INT NOT NULL,
    amount FLOAT NOT NULL,
    payment_date DATETIME NOT NULL,
    FOREIGN KEY (loan_id) REFERENCES loans(id)
);

-- DATA
INSERT INTO users (name, email, is_active) VALUES
('Alice Johnson', 'alice@example.com', TRUE),
('Bob Smith', 'bob@example.com', TRUE),
('Charlie Brown', 'charlie@example.com', TRUE),
('Diana Miller', 'diana@example.com', TRUE),
('Ethan Clark', 'ethan@example.com', FALSE),
('Fiona Davis', 'fiona@example.com', TRUE),
('George Wilson', 'george@example.com', TRUE),
('Hannah Moore', 'hannah@example.com', TRUE),
('Ian Taylor', 'ian@example.com', TRUE),
('Julia Anderson', 'julia@example.com', TRUE);

INSERT INTO authors (name) VALUES
('George Orwell'),
('J. K. Rowling'),
('J. R. R. Tolkien'),
('Agatha Christie'),
('Stephen King'),
('Isaac Asimov'),
('Arthur Conan Doyle'),
('Mark Twain'),
('Jane Austen'),
('Ernest Hemingway');

INSERT INTO books (title, published_year, available) VALUES
('1984', 1949, FALSE),
('Harry Potter and the Philosopher''s Stone', 1997, FALSE),
('The Hobbit', 1937, TRUE),
('Murder on the Orient Express', 1934, TRUE),
('The Shining', 1977, FALSE),
('Foundation', 1951, TRUE),
('Sherlock Holmes: A Study in Scarlet', 1887, TRUE),
('Adventures of Huckleberry Finn', 1884, TRUE),
('Pride and Prejudice', 1813, TRUE),
('The Old Man and the Sea', 1952, TRUE);

INSERT INTO book_authors (book_id, author_id) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);

INSERT INTO loans (user_id, book_id, loan_date, return_date, status) VALUES
(1, 1, '2025-12-01', NULL, 'BORROWED'),
(2, 2, '2025-11-20', '2025-12-05', 'RETURNED'),
(3, 5, '2025-11-10', NULL, 'OVERDUE'),
(4, 1, '2025-10-01', '2025-10-20', 'RETURNED'),
(5, 2, '2025-12-10', NULL, 'BORROWED'),
(6, 5, '2025-09-15', '2025-10-01', 'RETURNED'),
(7, 3, '2025-12-15', NULL, 'BORROWED'),
(8, 4, '2025-11-01', '2025-11-10', 'RETURNED'),
(9, 6, '2025-12-18', NULL, 'BORROWED'),
(10, 7, '2025-12-19', NULL, 'BORROWED');

INSERT INTO payments (loan_id, amount, payment_date) VALUES
(1, 50.0, '2025-12-01 10:00:00'),
(2, 20.0, '2025-12-05 14:30:00'),
(3, 75.5, '2025-11-15 09:00:00'),
(4, 10.0, '2025-10-20 16:45:00'),
(6, 15.0, '2025-10-01 11:20:00'),
(8, 25.0, '2025-11-10 13:10:00');

-- VIEWs
CREATE VIEW view_active_loans AS
SELECT 
    l.id AS loan_id,
    u.name AS user_name,
    b.title AS book_title,
    l.loan_date,
    l.status
FROM loans l
JOIN users u ON l.user_id = u.id
JOIN books b ON l.book_id = b.id
WHERE l.status = 'BORROWED';

select * from view_active_loans;


CREATE VIEW view_payment_summary AS
SELECT 
    u.id AS user_id,
    u.name,
    COUNT(p.id) AS payment_count,
    SUM(p.amount) AS total_paid,
    MIN(p.amount) AS min_payment,
    MAX(p.amount) AS max_payment
FROM users u
JOIN loans l ON u.id = l.user_id
JOIN payments p ON l.id = p.loan_id
GROUP BY u.id, u.name;

select * from view_payment_summary;

-- TRANSACTION
START TRANSACTION;

INSERT INTO loans (user_id, book_id, loan_date, status)
VALUES (1, 5, CURDATE(), 'BORROWED');

INSERT INTO payments (loan_id, amount, payment_date)
VALUES (LAST_INSERT_ID(), 50.0, NOW());

UPDATE books
SET available = false
WHERE id = 5;

COMMIT;

-- CHECK
select * from users;
select * from authors;
select * from books;
select * from book_authors;
select * from loans;
select * from payments;

drop table users;
drop table authors;
drop table books;
drop table book_authors;
drop table loans;
drop table payments;