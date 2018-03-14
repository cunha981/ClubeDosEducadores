package br.com.cde.lib.minerador.dao;

import java.time.LocalDate;
import java.util.List;

import org.springframework.dao.EmptyResultDataAccessException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.simple.SimpleJdbcCall;
import org.springframework.jdbc.datasource.DriverManagerDataSource;
import org.springframework.stereotype.Repository;

import br.com.cde.lib.minerador.model.VagaRemocao;

@Repository
public class VagaRemocaoDAO {
	private JdbcTemplate jdbcTemplate;

	public VagaRemocaoDAO() {
		DriverManagerDataSource ds = new DriverManagerDataSource();
		ds.setDriverClassName("com.mysql.jdbc.Driver");
		ds.setUrl("jdbc:mysql://localhost/ClubeDosEducadores?useSSL=false");
		ds.setUsername("root");
		ds.setPassword("147258");

		jdbcTemplate = new JdbcTemplate(ds);
	}

	public void save(List<VagaRemocao> vagasRemocao) {
		SimpleJdbcCall jdbcCall = new SimpleJdbcCall(jdbcTemplate);
		jdbcCall.withProcedureName("pr_crud_vagaremocao");

		vagasRemocao.parallelStream()
				.forEach(v -> jdbcCall.execute(v.getId() == null ? "C" : "U", v.getId(), v.getUnidadeId(),
						v.getCargoId(), v.getVagas(), v.getPeriodo(), v.getVagasPotenciais(), v.getVagasIniciais(),
						v.getTipoVagaId(), v.getDataIniciais(), v.getDataPotenciais()));
	}

	public Integer findIdByUnidadeIdAndCargoIdAndPeriodo(Integer unidadeId, Integer cargoId, LocalDate dataDO) {
		String sql = "SELECT Id FROM VagaRemocao WHERE UnidadeId = ? And CargoId = ? And Year(Periodo) = ?";
		Integer id = null;
		try {
			id = (Integer) jdbcTemplate.queryForObject(sql, new Object[] { unidadeId, cargoId, dataDO.getYear() },
					Integer.class);
		} catch (EmptyResultDataAccessException e) {
			return id;
		}catch (Exception e) {
			e.printStackTrace();
		}
		return id;
	}

}
