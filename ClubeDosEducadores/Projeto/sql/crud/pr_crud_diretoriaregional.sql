DELIMITER $$

DROP PROCEDURE IF EXISTS `pr_crud_diretoriaregional` $$

CREATE PROCEDURE `pr_crud_diretoriaregional`(IN _crud char(1),
	_id INT, 
	_nome nvarchar(200),
    _chave nvarchar(100),
    OUT _id_out int,
    OUT _erro varchar(200))

BEGIN

	IF (_crud = "C") Then
		INSERT INTO `DiretoriaRegional` (Nome, Chave) VALUES (_nome, _chave);
		SET _id_out = LAST_INSERT_ID();
	ELSEIF(_crud = "U") Then
		UPDATE `DiretoriaRegional` SET Nome = _nome, Chave = _chave WHERE `Id` = _id;
	ELSEIF (_crud = "D") Then
		delete from DiretoriaRegional WHERE Id = _id;
	ELSEIF (_crud = "R") Then
		SELECT * FROM DiretoriaRegional WHERE id = _id;
	END IF;

   END $$

DELIMITER ;


