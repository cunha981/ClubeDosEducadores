DELIMITER $$

DROP PROCEDURE IF EXISTS `pr_crud_funcionariounidadedistancia` $$

CREATE PROCEDURE `pr_crud_funcionariounidadedistancia`(IN _crud char(1),
	_funcionarioId INT, 
	_unidadeId int ,
	_distancia int,
    _duracao int,
    _custoTransporte decimal(15,2),
    _valido bit,
    _dtConsulta datetime,
    OUT _erro varchar(200))

BEGIN

	IF (_crud = "C") Then
		INSERT INTO `funcionariounidadedistancia` (`FuncionarioId`,`UnidadeId`,`Distancia`,`Duracao`,`CustoTransporte`,`Valido`,`DtConsulta`) VALUES
			(_funcionarioId,_unidadeId,_distancia,_duracao,_custoTransporte,_valido,_dtConsulta);	
	ELSE
		IF(_crud = "U") Then
			UPDATE `clubedoseducadores`.`funcionariounidadedistancia`
			SET `Distancia` = _distancia,`Duracao` = _duracao,`CustoTransporte` = _custoTransporte,`Valido` = _valido,`DtConsulta` = _dtConsulta
			WHERE `FuncionarioId` = _funcionarioId AND `UnidadeId` = _unidadeId;
	ELSEIF (_crud = "D") Then
			delete from funcionariounidadedistancia WHERE `FuncionarioId` = _funcionarioId AND `UnidadeId` = _unidadeId;
		ELSEIF (_crud = "R") Then
			SELECT * FROM funcionariounidadedistancia WHERE `FuncionarioId` = _funcionarioId AND `UnidadeId` = _unidadeId;
		END IF;
	END IF;

   END $$

DELIMITER ;


