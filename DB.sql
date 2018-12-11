CREATE DATABASE `13A_Borverseny`
  CHARACTER SET utf8
  COLLATE utf8_hungarian_ci;

USE `13A_Borverseny`;

CREATE TABLE Borasz(
  Id int(11) NOT NULL AUTO_INCREMENT,
  Nev varchar(100) NOT NULL UNIQUE,
  Email varchar(155),
  Telefon varchar(20) NOT NULL,
  PRIMARY KEY(Id)
);

CREATE TABLE Kategoria(
  Id int(11) NOT NULL,
  Megnevezes varchar(40) NOT NULL UNIQUE,
  PRIMARY KEY(Id)
);

INSERT Kategoria(Id, Megnevezes)
  VALUES(1, 'Fehér'),(2, 'Vörös'), (3, 'Rozé');

CREATE TABLE Nevezes(
  Id int(11) NOT NULL AUTO_INCREMENT,
  BoraszId int(11) NOT NULL,
  FantaziaNev varchar(50) NOT NULL,
  Borvidek varchar(100),
  Evjarat int(11) NOT NULL,
  KategoriaId int(11) NOT NULL,
  Helyezes int(2),
  CONSTRAINT FK_Nevezes_BoraszId FOREIGN KEY(BoraszId)
  REFERENCES Borasz(Id),
  CONSTRAINT FK_Nevezes_KategoriaId FOREIGN KEY(KategoriaId)
  REFERENCES Kategoria(Id),
  PRIMARY KEY(Id)
);

CREATE UNIQUE INDEX UX_Nevezes ON Nevezes(BoraszId, FantaziaNev, Evjarat);

CREATE PROCEDURE BoraszSelect(_Nev varchar(100))
  BEGIN
   SELECT * FROM Kategoria k
  WHERE Nev LIKE '%' + _Nev + '%';
  END;

INSERT INTO Borasz(Nev, Email, Telefon)
  VALUES (@Nev, @Email, @Telefon);

SELECT LAST_INSERT_ID();

