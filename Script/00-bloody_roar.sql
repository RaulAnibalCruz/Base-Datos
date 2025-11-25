-- Active: 1727132803198@@127.0.0.1@3306@5to_Bloody_Roar_2
SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema 5to_Bloody_Roar_2
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `5to_Bloody_Roar_2`;
CREATE SCHEMA IF NOT EXISTS `5to_Bloody_Roar_2`;
USE `5to_Bloody_Roar_2`;

-- -----------------------------------------------------
-- Table Personaje
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `5to_Bloody_Roar_2`.`Personaje` (
  `idPersonaje` INT NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(45) NOT NULL,
  `NombreBestia` VARCHAR(45) NOT NULL,
  `ResistenciaBestia` INT DEFAULT 10,
  PRIMARY KEY (`idPersonaje`)
) ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table Ataque
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `5to_Bloody_Roar_2`.`Ataque` (
  `idAtaque` INT NOT NULL AUTO_INCREMENT,
  `Tipo_Ataque` VARCHAR(45) NULL,
  `Danio` INT NULL,
  `idPersonaje` INT NOT NULL,
  PRIMARY KEY (`idAtaque`),
  CONSTRAINT `fk_Ataque_Personaje`
    FOREIGN KEY (`idPersonaje`)
    REFERENCES `5to_Bloody_Roar_2`.`Personaje` (`idPersonaje`)
    ON DELETE CASCADE
    ON UPDATE CASCADE
) ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table Usuario
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Usuario` (
  `idUsuario` INT NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(100),
  `Contrasenia` VARCHAR(256),
  `Email` VARCHAR(100),
  PRIMARY KEY (`idUsuario`),
  UNIQUE KEY uq_usuario_email (`Email`)
) ENGINE=InnoDB;





-- -----------------------------------------------------
-- Table ModoJuego
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `5to_Bloody_Roar_2`.`ModoJuego` (
  `idModoJuego` INT NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(45) NULL,
  PRIMARY KEY (`idModoJuego`)
) ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table Combate
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `5to_Bloody_Roar_2`.`Combate` (
  `idCombate` INT NOT NULL AUTO_INCREMENT,
  `idPersonaje` INT NOT NULL,
  `idUsuario` INT NOT NULL,
  `idModo_Juego` INT NOT NULL,
  `Duracion` INT NULL,
  PRIMARY KEY (`idCombate`),
  CONSTRAINT `fk_Partida_1`
    FOREIGN KEY (`idUsuario`)
    REFERENCES `5to_Bloody_Roar_2`.`Usuario` (`idUsuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Partida_2`
    FOREIGN KEY (`idModo_Juego`)
    REFERENCES `5to_Bloody_Roar_2`.`ModoJuego` (`idModoJuego`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Partida_3`
    FOREIGN KEY (`idPersonaje`)
    REFERENCES `5to_Bloody_Roar_2`.`Personaje` (`idPersonaje`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
) ENGINE = InnoDB;

CREATE INDEX `fk_Partida_1_idx` ON `5to_Bloody_Roar_2`.`Combate` (`idUsuario` ASC) VISIBLE;
CREATE INDEX `fk_Partida_2_idx` ON `5to_Bloody_Roar_2`.`Combate` (`idModo_Juego` ASC) VISIBLE;
CREATE INDEX `fk_Partida_3_idx` ON `5to_Bloody_Roar_2`.`Combate` (`idPersonaje` ASC) VISIBLE;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
