DELIMITER $$

DROP PROCEDURE IF EXISTS `pr_crud_unidade` $$

CREATE PROCEDURE `pr_crud_unidade`(IN _crud char(1),
	_id INT, 
    _nome nvarchar(200), 
	_codigo nvarchar(20),
    _chave nvarchar(100),
	_telefonePrincipal nvarchar(20),
	_telefoneSecundario nvarchar(20),
	_tipoUnidadeId int,
	_dificilAcesso int,
	_email nvarchar(200),
	_nomeDiarioOficial nvarchar(200),
	_regiaoUnidadeId int,
    _diretoriaRegionalId int,
	_url nvarchar(200),
    _uf nvarchar(2),
    _cidade nvarchar(50),
    _bairro nvarchar(50),
    _logradouro nvarchar(100),
    _cep nvarchar(10),
    _numero nvarchar(50),
    _endereco nvarchar(200),
    _latitude float,
    _longitude float,
    OUT _id_out int,
    OUT _erro varchar(200))

BEGIN

	IF (_crud = "C") Then
		INSERT INTO `clubedoseducadores`.`unidade` (`Nome`,`Codigo`,`Chave`,`TelefonePrincipal`,`TelefoneSecundario`,`TipoUnidadeId`,`DificilAcesso`,
			`Email`,`NomeDiarioOficial`,`RegiaoUnidadeId`,`DiretoriaRegionalId`,`Url`,`Uf`,`Cidade`,`Bairro`,`Logradouro`,`Cep`,`Numero`,`Endereco`,`Latitude`,`Longitude`)
		VALUES (_nome,_codigo,_chave,_telefonePrincipal,_telefoneSecundario,_tipoUnidadeId,_dificilAcesso,_email,_nomeDiarioOficial,
			_regiaoUnidadeId,_diretoriaRegionalId,_url,_uf,_cidade,_bairro,_logradouro,_cep,_numero,_endereco,_latitude,_longitude);
		
        SET _id_out = LAST_INSERT_ID();
	ELSE
		IF (_id is null or _id < 1) Then
			SET _erro = concat("Id é obrigatório para S/U/D - _id = ", _id);
		ELSEIF(_crud = "U") Then
			UPDATE `clubedoseducadores`.`unidade` SET
				`Nome` = _nome, `Codigo` = _codigo, `Chave` = _chave, `TelefonePrincipal` = _telefonePrincipal, `TelefoneSecundario` = _telefoneSecundario, 
				`TipoUnidadeId` = _tipoUnidadeId, `DificilAcesso` = _dificilAcesso, `Email` = _email, `NomeDiarioOficial` = _nomeDiarioOficial, 
				`RegiaoUnidadeId` = _regiaoUnidadeId, `DiretoriaRegionalId` = _diretoriaRegionalId, `Url` = _url, `Uf` = _uf, `Cidade` = _cidade, 
				`Bairro` = _bairro, `Logradouro` = _logradouro, `Cep` = _cep,  `Numero` = _numero, `Endereco` = _endereco, `Latitude` = _latitude,
				`Longitude` = _longitude
			WHERE `Id` = _id;
		ELSEIF (_crud = "D") Then
			update Funcionario set UnidadeTrabalhoId = null where UnidadeTrabalhoId = _id;
			delete from VagaRemocao where UnidadeId = _id;
			delete from FuncionarioUnidadeDistancia where UnidadeId = _id;
			delete from Unidade where Id = _id;
		ELSEIF (_crud = "R") Then
			select * from Unidade where id = _id;
		END IF;
	END IF;

   END $$

DELIMITER ;


