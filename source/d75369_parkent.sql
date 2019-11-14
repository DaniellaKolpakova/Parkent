-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Host: d75369.mysql.zonevs.eu
-- Loomise aeg: Okt 08, 2019 kell 02:43 PL
-- Serveri versioon: 10.2.27-MariaDB-log
-- PHP versioon: 7.2.18

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Andmebaas: `d75369_parkent`
--

-- --------------------------------------------------------

--
-- Tabeli struktuur tabelile `bankAccount`
--

CREATE TABLE `bankAccount` (
  `accountID` int(11) NOT NULL,
  `accountValue` float NOT NULL,
  `userID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tabeli struktuur tabelile `user`
--

CREATE TABLE `user` (
  `userID` int(11) NOT NULL,
  `login` varchar(50) NOT NULL,
  `name` varchar(50) NOT NULL,
  `surname` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `carNum` varchar(10) NOT NULL,
  `balance` float NOT NULL,
  `cardNumber` varchar(16) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tabeli struktuur tabelile `zone`
--

CREATE TABLE `zone` (
  `zoneID` int(11) NOT NULL,
  `zoneLoc` varchar(200) NOT NULL,
  `zonePrice` float NOT NULL,
  `zoneSpaces` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tabeli struktuur tabelile `zone_user`
--

CREATE TABLE `zone_user` (
  `zone_userID` int(11) NOT NULL,
  `userID` int(11) NOT NULL,
  `zoneID` int(11) NOT NULL,
  `start_date` datetime NOT NULL,
  `end_date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Indeksid tõmmistatud tabelitele
--

--
-- Indeksid tabelile `bankAccount`
--
ALTER TABLE `bankAccount`
  ADD PRIMARY KEY (`accountID`),
  ADD KEY `userID` (`userID`) USING BTREE;

--
-- Indeksid tabelile `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`userID`);

--
-- Indeksid tabelile `zone`
--
ALTER TABLE `zone`
  ADD PRIMARY KEY (`zoneID`);

--
-- Indeksid tabelile `zone_user`
--
ALTER TABLE `zone_user`
  ADD PRIMARY KEY (`zone_userID`),
  ADD KEY `userID` (`userID`) USING BTREE,
  ADD KEY `zoneID` (`zoneID`) USING BTREE;

--
-- AUTO_INCREMENT tõmmistatud tabelitele
--

--
-- AUTO_INCREMENT tabelile `bankAccount`
--
ALTER TABLE `bankAccount`
  MODIFY `accountID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT tabelile `user`
--
ALTER TABLE `user`
  MODIFY `userID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT tabelile `zone`
--
ALTER TABLE `zone`
  MODIFY `zoneID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT tabelile `zone_user`
--
ALTER TABLE `zone_user`
  MODIFY `zone_userID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Tõmmistatud tabelite piirangud
--

--
-- Piirangud tabelile `bankAccount`
--
ALTER TABLE `bankAccount`
  ADD CONSTRAINT `bankAccount_ibfk_1` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`);

--
-- Piirangud tabelile `zone_user`
--
ALTER TABLE `zone_user`
  ADD CONSTRAINT `zone_user_ibfk_2` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`),
  ADD CONSTRAINT `zone_user_ibfk_3` FOREIGN KEY (`zoneID`) REFERENCES `zone` (`zoneID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
