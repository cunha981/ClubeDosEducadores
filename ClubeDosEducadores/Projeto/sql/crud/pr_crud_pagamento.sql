DELIMITER $$

DROP PROCEDURE IF EXISTS `pr_crud_pagamento` $$

CREATE PROCEDURE `pr_crud_pagamento`(IN _crud char(1),
	_id INT, 
	_funcionarioId int ,
	_valor decimal(15,2),
	_desconto decimal(15,2),
	_valorLiquido decimal(15,2),
	_observacao nvarchar(300),
	_dtCompra datetime,
    _dtPagamento datetime,
    _formaPagamentoId int,
    _produto nvarchar(200),
    _tipoProdutoId int,
    OUT _id_out int,
    OUT _erro varchar(200))

BEGIN

	IF (_crud = "C") Then
		INSERT INTO pagamento (`FuncionarioId`,`Valor`,`Desconto`,`ValorLiquido`,`Observacao`,`DtCompra`,`DtPamento`,`FormaPagamentoId`,`Produto`,`TipoProdutoId`) VALUES
			(_funcionarioId,_valor,_desconto,_valorLiquido,_observacao,_dtCompra,_dtPagamento,_formaPagamentoId,_produto,_tipoProdutoId);

        SET _id_out = LAST_INSERT_ID();
	ELSEIF(_crud = "U") Then
		UPDATE `pagamento`
        SET `Valor` = _valor,`Desconto` = _desconto,`ValorLiquido` = _valorLiquido,`Observacao` = _observacao,`DtCompra` = _dtCompra,`DtPamento` = _dtPagamento,`FormaPagamentoId` = _formaPagamentoId
		WHERE `Id` = _id;
	ELSEIF (_crud = "D") Then
		delete from pagamento WHERE Id = _id;
	ELSEIF (_crud = "R") Then
		SELECT * FROM pagamento WHERE id = _id;
	END IF;

   END $$

DELIMITER ;


