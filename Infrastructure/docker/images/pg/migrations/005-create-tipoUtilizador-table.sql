CREATE TABLE tipoUtilizador(
                               tipo_id SERIAL PRIMARY KEY,
                               tipo VARCHAR (100),
                               participante_id INT REFERENCES participantes(id)
);
