USE `5to_Bloody_Roar_2`;

DELIMITER $$
DROP FUNCTION IF EXISTS ObtenerPersonajePorCombate;
CREATE FUNCTION ObtenerPersonajePorCombate (p_idCombate INT) 
    RETURNS INT
    READS SQL DATA
BEGIN
    DECLARE personajeId INT;

    -- Consultar el idPersonaje asociado al idCombate dado
    SELECT idPersonaje INTO personajeId
    FROM Combate
    WHERE idCombate = p_idCombate;

    RETURN personajeId;
END $$

DELIMITER $$
DROP FUNCTION IF EXISTS PersonajeConMasDanioEnCombate;
CREATE FUNCTION PersonajeConMasDanioEnCombate (p_idCombate INT) 
    RETURNS INT
    READS SQL DATA
BEGIN
    DECLARE personajeId INT;

    -- Consultar el idPersonaje que hizo más daño en el combate dado
    SELECT `idPersonaje` INTO personajeId
    FROM Combate
    WHERE idCombate = p_idCombate
    GROUP BY idPersonaje
    ORDER BY SUM(`Danio`) DESC
    LIMIT 1;

    RETURN personajeId;
END $$


-- Llamar a la función para obtener el idPersonaje asociado al idCombate 1
-- SELECT ObtenerPersonajePorCombate (1);

-- Llamar a la función para obtener el idPersonaje con mas daño asociado al idCombate 1
-- SELECT PersonajeConMasDanioEnCombate(1);

