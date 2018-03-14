DELIMITER $$

DROP PROCEDURE IF EXISTS `pr_crud_vagaremocao` $$

CREATE PROCEDURE `pr_crud_vagaremocao`(IN _crud char(1),
	_id INT, 
	_unidadeId int ,
	_cargoId int ,
	_vagas int ,
	_periodo date ,
	_vagasPotenciais int,
	_vagasIniciais int,
    _tipoVagaId int ,
    _dtIniciais datetime,
    _dtPotenciais datetime,
    OUT _id_out int,
    OUT _erro varchar(200))

BEGIN

	IF (_crud = "C") Then
		insert into VagaRemocao (UnidadeId, CargoId, Periodo, Vagas, VagasIniciais, VagasPotenciais, TipoVagaId, DtIniciais) values
			(_unidadeId, _cargoId, _periodo, 
				if(_vagasIniciais IS NULL, 0, _vagasIniciais) + 
				if(_vagasPotenciais IS NULL, 0, _vagasPotenciais),
				_vagasIniciais, _vagasPotenciais, _tipoVagaId, now());
		
        SET _id_out = LAST_INSERT_ID();
	ELSE
		IF(_crud = "U") Then
			
            IF(_tipoVagaId = 1) Then -- Remocao Efetivo
				UPDATE vagaremocao SET 
					VagasIniciais = if(_vagasIniciais IS NULL, VagasIniciais, _vagasIniciais),
                    VagasPotenciais = if(_vagasPotenciais IS NULL, VagasPotenciais, _vagasPotenciais),
                    DtIniciais = if(_dtIniciais IS NULL, DtIniciais, _dtIniciais),
                    DtPotenciais = if(_dtPotenciais IS NULL, DtPotenciais, _dtPotenciais),
                    Vagas = if(_vagasIniciais IS NULL, if(VagasIniciais IS NULL, 0, VagasIniciais), _vagasIniciais) + 
							if(_vagasPotenciais IS NULL, if(VagasPotenciais IS NULL, 0, VagasPotenciais), _vagasPotenciais)
                WHERE Id = _id;
            ELSEIF(_tipoVagaId = 2) Then
				UPDATE vagaremocao SET DtIniciais = now(), Vagas = _vagas WHERE Id = _id;
            END IF;
		ELSEIF (_crud = "D") Then
			delete from VagaRemocao WHERE Id = _id;
		ELSEIF (_crud = "R") Then
			SELECT * FROM VagaRemocao WHERE (Id > 0 AND id = _id) OR (TipoVaga = _tipoVaga AND Periodo = _periodo);
		END IF;
	END IF;

   END $$

DELIMITER ;


