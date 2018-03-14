DELIMITER $$

DROP PROCEDURE IF EXISTS `pr_crud_tipovaga` $$

CREATE PROCEDURE `pr_crud_tipovaga`(IN _crud char(1),
	_id INT, 
	_descricao nvarchar(200),
    OUT _id_out int,
    OUT _erro varchar(200))

BEGIN

	IF (_crud = "C") Then
		INSERT INTO `TipoVaga` (`Descricao`) VALUES (_descricao);
		SET _id_out = LAST_INSERT_ID();
	ELSEIF(_crud = "U") Then
		UPDATE `TipoVaga` SET Descricao = _descricao WHERE `Id` = _id;
	ELSEIF (_crud = "D") Then
		delete from TipoVaga WHERE Id = _id;
	ELSEIF (_crud = "R") Then
		SELECT * FROM TipoVaga WHERE id = _id;
	END IF;

   END $$

DELIMITER ;


