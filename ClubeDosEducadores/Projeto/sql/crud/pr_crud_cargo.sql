DELIMITER $$

DROP PROCEDURE IF EXISTS `pr_crud_cargo` $$

CREATE PROCEDURE `pr_crud_cargo`(IN _crud char(1),
	_id INT, 
	_descricao nvarchar(200),
    _abreviacao nvarchar(20),
    _codigo nvarchar(100),
    OUT _id_out int,
    OUT _erro varchar(200))

BEGIN

	IF (_crud = "C") Then
		INSERT INTO `Cargo` (Descricao, Abreviacao, Chave) VALUES (_descricao, _abreviacao, _codigo);
		SET _id_out = LAST_INSERT_ID();
	ELSEIF(_crud = "U") Then
		UPDATE `Cargo` SET Descricao = _descricao, Abreviacao = _abreviacao, Chave = _codigo WHERE `Id` = _id;
	ELSEIF (_crud = "D") Then
		delete from Cargo WHERE Id = _id;
	ELSEIF (_crud = "R") Then
		SELECT * FROM Cargo WHERE id = _id;
	END IF;

   END $$

DELIMITER ;


