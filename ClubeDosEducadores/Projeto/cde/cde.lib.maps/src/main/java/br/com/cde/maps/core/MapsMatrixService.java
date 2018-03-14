package br.com.cde.maps.core;

import javax.ws.rs.client.Client;
import javax.ws.rs.client.ClientBuilder;
import javax.ws.rs.core.MediaType;

import br.com.cde.maps.exceptions.EmptyValueException;
import br.com.cde.maps.exceptions.InvalidValueException;
import br.com.cde.maps.exceptions.LimitExceededException;
import br.com.cde.maps.model.MapsMatrixResult;

public class MapsMatrixService {

	private static final String MATRIX_API_KEY_1 = "AIzaSyAidxwe04OUlUSEM8us3o9xGkSKgDP9XIk";

	private static final String REST_URI = "https://maps.googleapis.com/maps/api/distancematrix/json?";

	private Client client = ClientBuilder.newClient();

	public MapsMatrixResult getDistances(String origin, String[] destinations, boolean useTransitMode) throws EmptyValueException, LimitExceededException, InvalidValueException {
		if (origin == null || origin.isEmpty()) {
			throw new EmptyValueException("origin");
		}

		if (destinations == null) {
			throw new EmptyValueException("destinations");
		}

		if (destinations.length > 25) {
			throw new LimitExceededException("destinations", 25);
		}

		String destinationsFinal = null;

		for (String destination : destinations) {
			if (destination == null) {
				throw new EmptyValueException("destination");
			}
			
			if(destination.contains("|")) {
				throw new InvalidValueException("destination");
			}

			if (destinationsFinal == null) {
				destinationsFinal = destination;
				continue;
			}

			destinationsFinal += "|" + destination;
		}

		return client
				.target(REST_URI)
				.queryParam("key", MATRIX_API_KEY_1)
				.queryParam("origins", origin)
				.queryParam("destinations", destinationsFinal)
				.queryParam("mode", useTransitMode ? "transit" : "driving")
				.queryParam("language", "pt-BR")
				.queryParam("units", "metric")
				.request(MediaType.APPLICATION_JSON)
				.get(MapsMatrixResult.class);

	}

}
