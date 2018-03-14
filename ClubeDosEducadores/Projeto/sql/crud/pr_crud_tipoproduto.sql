DELIMITER $$

DROP PROCEDURE IF EXISTS `pr_crud_tipoproduto` $$

CREATE PROCEDURE `pr_crud_tipoproduto`(IN _crud char(1),
	_id INT, 
	_descricao nvarchar(200),
    OUT _id_out int,
    OUT _erro varchar(200))

BEGIN

	IF (_crud = "C") Then
		INSERT INTO `TipoProduto` (`Descricao`) VALUES (_descricao);
		SET _id_out = LAST_INSERT_ID();
	ELSEIF(_crud = "U") Then
		UPDATE `TipoProduto` SET Descricao = _descricao WHERE `Id` = _id;
	ELSEIF (_crud = "D") Then
		delete from TipoProduto WHERE Id = _id;
	ELSEIF (_crud = "R") Then
		SELECT * FROM TipoProduto WHERE id = _id;
	END IF;

   END $$

DELIMITER ;


