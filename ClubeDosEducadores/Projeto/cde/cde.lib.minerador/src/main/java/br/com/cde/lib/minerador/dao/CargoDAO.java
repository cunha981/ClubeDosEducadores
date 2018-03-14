package br.com.cde.lib.minerador.dao;

import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.datasource.DriverManagerDataSource;
import org.springframework.stereotype.Repository;

@Repository
public class CargoDAO {

	private JdbcTemplate jdbcTemplate;

    public CargoDAO() {
		DriverManagerDataSource ds = new DriverManagerDataSource();
	    ds.setDriverClassName("com.mysql.jdbc.Driver");
	    ds.setUrl("jdbc:mysql://localhost/ClubeDosEducadores?useSSL=false");
	    ds.setUsername("root");
	    ds.setPassword("147258");
	    
        jdbcTemplate = new JdbcTemplate(ds);
    }
	public Integer findCargoIdByCodCargo(String substring) {
		String sql = "SELECT Id FROM Cargo WHERE Codigo = ?";
		
		return (Integer)jdbcTemplate.queryForObject(sql, new Object[] {substring},Integer.class);
	}

}
