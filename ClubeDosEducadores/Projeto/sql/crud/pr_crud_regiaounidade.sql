DELIMITER $$

DROP PROCEDURE IF EXISTS `pr_crud_regiaounidade` $$

CREATE PROCEDURE `pr_crud_regiaounidade`(IN _crud char(1),
	_id INT, 
	_regiao nvarchar(200),
    _chave nvarchar(100),
    OUT _id_out int,
    OUT _erro varchar(200))

BEGIN

	IF (_crud = "C") Then
		INSERT INTO `RegiaoUnidade` (Regiao, Chave) VALUES (_regiao, _chave);
		SET _id_out = LAST_INSERT_ID();
	ELSEIF(_crud = "U") Then
		UPDATE `RegiaoUnidade` SET Regiao = _regiao, Chave = _chave WHERE `Id` = _id;
	ELSEIF (_crud = "D") Then
		delete from RegiaoUnidade WHERE Id = _id;
	ELSEIF (_crud = "R") Then
		SELECT * FROM RegiaoUnidade WHERE id = _id;
	END IF;

   END $$

DELIMITER ;


