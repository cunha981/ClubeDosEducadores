package br.com.cde.lib.minerador.dao.mapper;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import br.com.cde.lib.minerador.model.Unidade;

public class UnidadeRowMapper implements RowMapper<Unidade>{

	@Override
	public Unidade mapRow(ResultSet rs, int rowNum) throws SQLException {
		Unidade unidade = new Unidade();
		
		unidade.setId(rs.getInt("Id"));
		unidade.setNome(rs.getString("Nome"));
		
		return unidade;
	}

}
