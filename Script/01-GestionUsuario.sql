-- Active: 1691412339871@@127.0.0.1@3306@5to_Bloody_Roar_2
USE `5to_Bloody_Roar_2`;

DROP USER IF EXISTS 'administrador'@'localhost';
CREATE USER 'administrador'@'localhost' IDENTIFIED BY 'PassAdmin123.';

DROP USER IF EXISTS 'usuario'@'%';
CREATE USER 'usuario'@'%' IDENTIFIED BY 'PassUsuario123.';


GRANT SELECT ON 5to_Bloody_Roar_2.Combate TO 'usuario'@'%';
GRANT SELECT,UPDATE,DELETE on 5to_Bloody_Roar_2.* TO 'administrador'@'localhost';