use CryptoDb_ARZ5PC
go
select * from CryptoAssets
select * from VirtualWallets
select * from Users
select * from CryptoCurrencies


INSERT INTO Users (Name, Email, Password)
VALUES 
('John Doe', 'john.doe@example.com', 'password123'),
('Jane Smith', 'jane.smith@example.com', 'password456');


INSERT INTO VirtualWallets (Amount, UserId)
VALUES 
(1000.0, 2)
  

INSERT INTO CryptoAssets (Name, Price, VirtualWalletId)
VALUES 
('BTC', 0.5, 1), 
('ETH', 2.0, 1); 


INSERT INTO CryptoAssets (Name, Price, VirtualWalletId)
VALUES 
('BTC', 1.0, 9),  
('LTC', 10.0, 9); 







insert into CryptoCurrencies VALUES('BTC', 'Bitcoin', 3000, 100)
insert into CryptoCurrencies VALUES('ETH', 'Ethereum', 300, 221)
insert into CryptoCurrencies VALUES('USDT', 'Tether', 2400, 300)
insert into CryptoCurrencies VALUES('Binance Coin', 'BNB', 500, 54)
insert into CryptoCurrencies VALUES('SOL', 'Solana', 100, 244)
insert into CryptoCurrencies VALUES('Ripple', 'XRP', 200, 300)
insert into CryptoCurrencies VALUES('ADA', 'Cardano', 30, 700)
insert into CryptoCurrencies VALUES('DOGE', 'Dodgecoin', 10, 500)
insert into CryptoCurrencies VALUES('DOT', 'Polkadot', 400, 150)
insert into CryptoCurrencies VALUES('AVAX', 'Avalache', 150, 300)
insert into CryptoCurrencies VALUES('TRX', 'Tron', 400, 500)
insert into CryptoCurrencies VALUES('LTC', 'Litecoin', 3000, 100)
insert into CryptoCurrencies VALUES('LINK', 'Shiba', 3000, 100)
insert into CryptoCurrencies VALUES('SHIB', 'Shiba', 3000, 100)
insert into CryptoCurrencies VALUES('UNI', 'Uniswap', 3000, 100)