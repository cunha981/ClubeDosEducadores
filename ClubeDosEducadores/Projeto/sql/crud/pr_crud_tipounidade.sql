DELIMITER $$

DROP PROCEDURE IF EXISTS `pr_crud_tipounidade` $$

CREATE PROCEDURE `pr_crud_tipounidade`(IN _crud char(1),
	_id INT, 
	_descricao nvarchar(200),
    _tipo nvarchar(20),
    _chave nvarchar(100),
    OUT _id_out int,
    OUT _erro varchar(200))

BEGIN

	IF (_crud = "C") Then
		INSERT INTO `TipoUnidade` (Descricao, Tipo, Chave) VALUES (_descricao, _tipo, _chave);
		SET _id_out = LAST_INSERT_ID();
	ELSEIF(_crud = "U") Then
		UPDATE `TipoUnidade` SET Descricao = _descricao, Tipo = _tipo, Chave = _chave WHERE `Id` = _id;
	ELSEIF (_crud = "D") Then
		delete from TipoUnidade WHERE Id = _id;
	ELSEIF (_crud = "R") Then
		SELECT * FROM TipoUnidade WHERE id = _id;
	END IF;

   END $$

DELIMITER ;


