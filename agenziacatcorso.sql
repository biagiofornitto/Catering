-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Creato il: Nov 03, 2017 alle 23:20
-- Versione del server: 10.1.21-MariaDB
-- Versione PHP: 5.6.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `agenziacatcorso`
--
CREATE DATABASE `agenziacatcorso`;
-- --------------------------------------------------------

--
-- Struttura della tabella `catering`
--

CREATE TABLE `catering` (
  `Id` int(11) NOT NULL,
  `Cliente` longtext,
  `Codice` int(11) NOT NULL,
  `CostoT` float NOT NULL,
  `Data` datetime(6) NOT NULL,
  `Menu` int(11) NOT NULL,
  `NumeroP` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dump dei dati per la tabella `catering`
--

INSERT INTO `catering` (`Id`, `Cliente`, `Codice`, `CostoT`, `Data`, `Menu`, `NumeroP`) VALUES
(1, '3', 1, 500, '2012-05-07 00:00:00.000000', 1, 3),
(2, '4', 2, 500, '2013-05-07 00:00:00.000000', 2, 4),
(3, '2', 3, 500, '2013-05-08 00:00:00.000000', 2, 2),
(4, '1', 4, 650, '2011-05-06 00:00:00.000000', 2, 1),
(5, '3', 5, 700, '2013-05-08 00:00:00.000000', 2, 3),
(6, '2', 6, 700, '2016-05-08 00:00:00.000000', 4, 2),
(7, '2', 7, 900, '2013-05-08 00:00:00.000000', 5, 2),
(8, '2', 8, 200, '2015-05-08 00:00:00.000000', 3, 2);

--
-- Trigger `catering`
--
DELIMITER $$
CREATE TRIGGER `aggiorna_delete_n_catering` AFTER DELETE ON `catering` FOR EACH ROW begin
update cliente c
set N_Catering=N_Catering-1, Spesa_Totale=Spesa_Totale-OLD.CostoT
where c.Id=old.Cliente;
end
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `aggiorna_n_catering` AFTER INSERT ON `catering` FOR EACH ROW begin
update cliente c
set N_Catering=N_Catering+1, Spesa_Totale=Spesa_Totale+NEW.CostoT
where c.Id=new.Cliente;
end
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Struttura della tabella `cliente`
--

CREATE TABLE `cliente` (
  `Id` int(11) NOT NULL,
  `Citta` longtext,
  `Codice_Fiscale` longtext,
  `Cognome` longtext,
  `Indirizzo` longtext,
  `N_Catering` int(11) NOT NULL,
  `Nome` longtext,
  `Spesa_Totale` float NOT NULL,
  `Telefono` longtext,
  `Username` longtext
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dump dei dati per la tabella `cliente`
--

INSERT INTO `cliente` (`Id`, `Citta`, `Codice_Fiscale`, `Cognome`, `Indirizzo`, `N_Catering`, `Nome`, `Spesa_Totale`, `Telefono`, `Username`) VALUES
(1, 'maletto', '1', 'papale', 'via milano', 3, 'iano', 500, '3807467134', 'fabio'),
(2, 'catania', '2', 'zina', 'via catania', 5, 'giacomo', 1000, '3807411165', 'marco'),
(3, 'bronte', '3', 'po', 'via catania', 2, 'jenny', 1500, '3807411166', 'biagio'),
(4, 'ravenna', '4', 'ferro', 'via verdi', 1, 'tano', 2000, '3807411167', 'tanazzo');

-- --------------------------------------------------------

--
-- Struttura della tabella `invitato`
--

CREATE TABLE `invitato` (
  `Id` int(11) NOT NULL,
  `Cognome` longtext,
  `IdCat` int(11) NOT NULL,
  `Nome` longtext
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dump dei dati per la tabella `invitato`
--

INSERT INTO `invitato` (`Id`, `Cognome`, `IdCat`, `Nome`) VALUES
(1, 'rossi', 1, 'guido'),
(2, 'rossi', 1, 'gabriella'),
(3, 'falsaperla', 1, 'saro'),
(4, 'papale', 2, 'iano'),
(5, 'papale', 2, 'filippo'),
(6, 'papale', 2, 'salvo'),
(7, 'papale', 2, 'santo'),
(8, 'catania', 3, 'giuseppe'),
(9, 'papale', 3, 'giuseppe'),
(10, 'catania', 4, 'peppino'),
(11, 'ferrari', 5, 'fabio'),
(12, 'papale', 5, 'giuseppe'),
(13, 'finocchiaro', 5, 'giuseppe'),
(14, 'capizzi', 6, 'giuseppe'),
(15, 'mineo', 6, 'giuseppe'),
(16, 'mercedes', 7, 'giuseppe'),
(17, 'mineo', 7, 'giuseppe'),
(18, 'mineo', 8, 'marco'),
(19, 'mineo', 8, 'maria');

-- --------------------------------------------------------

--
-- Struttura della tabella `menu`
--

CREATE TABLE `menu` (
  `Id` int(11) NOT NULL,
  `Costo_Totale` float NOT NULL,
  `IdRistorante` longtext
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dump dei dati per la tabella `menu`
--

INSERT INTO `menu` (`Id`, `Costo_Totale`, `IdRistorante`) VALUES
(1, 50, '1'),
(2, 15, '1'),
(3, 40, '2'),
(4, 150, '3'),
(5, 200, '4');

-- --------------------------------------------------------

--
-- Struttura della tabella `portata`
--

CREATE TABLE `portata` (
  `Id` int(11) NOT NULL,
  `Costo` float NOT NULL,
  `IdMenu` int(11) NOT NULL,
  `Nome` longtext
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dump dei dati per la tabella `portata`
--

INSERT INTO `portata` (`Id`, `Costo`, `IdMenu`, `Nome`) VALUES
(1, 25, 1, 'pasta'),
(2, 20, 1, 'broccoli'),
(3, 20, 1, 'carote'),
(4, 50, 2, 'pesce'),
(5, 70, 4, 'carne'),
(6, 30, 3, 'patate'),
(7, 15, 3, 'gelato');

--
-- Trigger `portata`
--
DELIMITER $$
CREATE TRIGGER `aggiorna_costo_menu` AFTER INSERT ON `portata` FOR EACH ROW begin
update menu m set Costo_Totale = coalesce(Costo_totale,0) + (select Costo from portata where Id =NEW.Id) where m.Id = NEW.Id;
end
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Struttura della tabella `ristorante`
--

CREATE TABLE `ristorante` (
  `Id` int(11) NOT NULL,
  `Indirizzo` longtext,
  `Piva` longtext,
  `Username` longtext
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dump dei dati per la tabella `ristorante`
--

INSERT INTO `ristorante` (`Id`, `Indirizzo`, `Piva`, `Username`) VALUES
(1, 'via verona', '1', 'marco'),
(2, 'via milano', '2', 'pizza'),
(3, 'via catania', '3', 'pescivendolo'),
(4, 'via milano', '4', 'geco');

-- --------------------------------------------------------

--
-- Struttura della tabella `utente`
--

CREATE TABLE `utente` (
  `Id` int(11) NOT NULL,
  `Password` longtext,
  `Tipo` int(11) NOT NULL,
  `Username` longtext
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dump dei dati per la tabella `utente`
--

INSERT INTO `utente` (`Id`, `Password`, `Tipo`, `Username`) VALUES
(1, '73c18c59a39b18382081ec00bb456d43', 1, 'biagio'),
(2, '73c18c59a39b18382081ec00bb456d43', 2, 'marco'),
(3, '73c18c59a39b18382081ec00bb456d43', 3, 'fabio'),
(4, '73c18c59a39b18382081ec00bb456d43', 3, 'matrix'),
(5, '73c18c59a39b18382081ec00bb456d43', 3, 'iano'),
(6, '73c18c59a39b18382081ec00bb456d43', 3, 'tanazzo'),
(7, '73c18c59a39b18382081ec00bb456d43', 2, 'geco'),
(8, '73c18c59a39b18382081ec00bb456d43', 2, 'pizza'),
(9, '73c18c59a39b18382081ec00bb456d43', 2, 'pescivendolo');

--
-- Indici per le tabelle scaricate
--

--
-- Indici per le tabelle `catering`
--
ALTER TABLE `catering`
  ADD PRIMARY KEY (`Id`);

--
-- Indici per le tabelle `cliente`
--
ALTER TABLE `cliente`
  ADD PRIMARY KEY (`Id`);

--
-- Indici per le tabelle `invitato`
--
ALTER TABLE `invitato`
  ADD PRIMARY KEY (`Id`);

--
-- Indici per le tabelle `menu`
--
ALTER TABLE `menu`
  ADD PRIMARY KEY (`Id`);

--
-- Indici per le tabelle `portata`
--
ALTER TABLE `portata`
  ADD PRIMARY KEY (`Id`);

--
-- Indici per le tabelle `ristorante`
--
ALTER TABLE `ristorante`
  ADD PRIMARY KEY (`Id`);

--
-- Indici per le tabelle `utente`
--
ALTER TABLE `utente`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT per le tabelle scaricate
--

--
-- AUTO_INCREMENT per la tabella `catering`
--
ALTER TABLE `catering`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT per la tabella `cliente`
--
ALTER TABLE `cliente`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT per la tabella `invitato`
--
ALTER TABLE `invitato`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;
--
-- AUTO_INCREMENT per la tabella `menu`
--
ALTER TABLE `menu`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT per la tabella `portata`
--
ALTER TABLE `portata`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
--
-- AUTO_INCREMENT per la tabella `ristorante`
--
ALTER TABLE `ristorante`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT per la tabella `utente`
--
ALTER TABLE `utente`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
