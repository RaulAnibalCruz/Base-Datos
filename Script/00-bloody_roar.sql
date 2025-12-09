-- =============================================
-- BASE DE DATOS BLOODY ROAR 2 - VERSIÓN FINAL CORREGIDA
-- CON ON DELETE CASCADE EN TODAS LAS FK
-- =============================================

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

DROP SCHEMA IF EXISTS `5to_Bloody_Roar_2`;
CREATE SCHEMA IF NOT EXISTS `5to_Bloody_Roar_2`;
USE `5to_Bloody_Roar_2`;

-- -----------------------------------------------------
-- Tabla Personaje
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Personaje` (
  `idPersonaje` INT NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(45) NOT NULL,
  `NombreBestia` VARCHAR(45) NOT NULL,
  `ResistenciaBestia` INT DEFAULT 10,
  PRIMARY KEY (`idPersonaje`)
) ENGINE = InnoDB;

-- -----------------------------------------------------
-- Tabla Ataque → borra en cascada si se borra el personaje
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Ataque` (
  `idAtaque` INT NOT NULL AUTO_INCREMENT,
  `Tipo_Ataque` VARCHAR(45) NULL,
  `Danio` INT NULL,
  `idPersonaje` INT NOT NULL,
  PRIMARY KEY (`idAtaque`),
  INDEX `fk_Ataque_Personaje_idx` (`idPersonaje` ASC) VISIBLE,
  CONSTRAINT `fk_Ataque_Personaje`
    FOREIGN KEY (`idPersonaje`)
    REFERENCES `Personaje` (`idPersonaje`)
    ON DELETE CASCADE
    ON UPDATE CASCADE
) ENGINE = InnoDB;

-- -----------------------------------------------------
-- Tabla Usuario
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Usuario` (
  `idUsuario` INT NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(100) NULL,
  `Contrasenia` VARCHAR(256) NULL,
  `Email` VARCHAR(100) NULL,
  PRIMARY KEY (`idUsuario`),
  UNIQUE INDEX `uq_usuario_email` (`Email` ASC) VISIBLE
) ENGINE = InnoDB;

-- -----------------------------------------------------
-- Tabla ModoJuego
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ModoJuego` (
  `idModoJuego` INT NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(45) NULL,
  PRIMARY KEY (`idModoJuego`)
) ENGINE = InnoDB;

-- -----------------------------------------------------
-- Tabla Combate → ON DELETE CASCADE EN LAS 3 FK
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Combate` (
  `idCombate` INT NOT NULL AUTO_INCREMENT,
  `idPersonaje` INT NOT NULL,
  `idUsuario` INT NOT NULL,
  'fecha_hora' datetime not null,
  `idModo_Juego` INT NOT NULL,
  `Duracion` INT NULL,
  PRIMARY KEY (`idCombate`),
  
  INDEX `fk_Combate_Personaje_idx` (`idPersonaje` ASC) VISIBLE,
  INDEX `fk_Combate_Usuario_idx` (`idUsuario` ASC) VISIBLE,
  INDEX `fk_Combate_ModoJuego_idx` (`idModo_Juego` ASC) VISIBLE,

  CONSTRAINT `fk_Combate_Personaje`
    FOREIGN KEY (`idPersonaje`)
    REFERENCES `Personaje` (`idPersonaje`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,

  CONSTRAINT `fk_Combate_Usuario`
    FOREIGN KEY (`idUsuario`)
    REFERENCES `Usuario` (`idUsuario`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,

  CONSTRAINT `fk_Combate_ModoJuego`
    FOREIGN KEY (`idModo_Juego`)
    REFERENCES `ModoJuego` (`idModoJuego`)
    ON DELETE CASCADE
    ON UPDATE CASCADE
) ENGINE = InnoDB;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- =============================================
-- FIN - AHORA PUEDES BORRAR USUARIOS, PERSONAJES Y MODOS
-- Y TODOS SUS DATOS RELACIONADOS SE BORRAN SOLOS
-- =============================================