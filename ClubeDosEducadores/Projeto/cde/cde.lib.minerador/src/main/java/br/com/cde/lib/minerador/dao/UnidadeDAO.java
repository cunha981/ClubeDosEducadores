package br.com.cde.lib.minerador.dao;

import org.springframework.dao.EmptyResultDataAccessException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.datasource.DriverManagerDataSource;
import org.springframework.stereotype.Repository;

import br.com.cde.lib.minerador.dao.mapper.UnidadeRowMapper;
import br.com.cde.lib.minerador.model.Unidade;

@Repository
public class UnidadeDAO {

	private JdbcTemplate jdbcTemplate;

	public UnidadeDAO() {
		DriverManagerDataSource ds = new DriverManagerDataSource();
		ds.setDriverClassName("com.mysql.jdbc.Driver");
		ds.setUrl("jdbc:mysql://localhost/ClubeDosEducadores?useSSL=false");
		ds.setUsername("root");
		ds.setPassword("147258");

		jdbcTemplate = new JdbcTemplate(ds);
	}

	public Unidade findUnidadeByName(String nome) {
		Unidade unidade = null;
		String sql = "SELECT * FROM Unidade WHERE NomeDiarioOficial = ?";
		try {
			unidade = (Unidade) jdbcTemplate.queryForObject(sql, new Object[] { nome }, new UnidadeRowMapper());
		} catch (EmptyResultDataAccessException e) {
			return unidade;
		}
		return unidade;
	}

	public long findDiretoriaRegionalIdByChave(String chave) {
		String sql = "SELECT Id FROM DiretoriaRegional WHERE Chave = ?";

		return (Integer) jdbcTemplate.queryForObject(sql, new Object[] { chave }, Integer.class);
	}

	public long findRegiaoUnidadeIdByChave(String chave) {
		String sql = "SELECT Id FROM RegiaoUnidade WHERE Chave = ?";

		return (Integer) jdbcTemplate.queryForObject(sql, new Object[] { chave }, Integer.class);
	}

	public long findTipoUnidadeIdByTipo(String tipoUnidade) {
		String sql = "SELECT Id FROM TipoUnidade WHERE Tipo = ?";

		return (Integer) jdbcTemplate.queryForObject(sql, new Object[] { tipoUnidade }, Integer.class);
	}

	public void save(Unidade u) {
		String sql = "INSERT INTO Unidade (Nome, Chave, Codigo, TelefonePrincipal, TipoUnidadeId, Email, NomeDiarioOficial, RegiaoUnidadeId, "
				+ "DiretoriaRegionalId, Url, Uf, Cidade, Bairro, Logradouro, Cep, Numero, Endereco, Latitude, Longitude) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
		jdbcTemplate.update(sql,
				new Object[] { u.getNome(), "", u.getCodigo(), u.getTelPrincipal(), u.getTipoUnidadeId(), u.getEmail(), u.getNome(),
						u.getRegiaoUnidadeId(), u.getDiretoriaregionalId(), u.getUrl(), u.getUf(), u.getCidade(),
						u.getBairro(), u.getLogradouro(), u.getCep(), u.getNumero(), u.getEndereco(), u.getLatitude(),
						u.getLongitude() });

	}

}
