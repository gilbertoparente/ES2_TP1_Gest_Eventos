CREATE TABLE participantes (
                               id SERIAL PRIMARY KEY,
                               tipoUtilizador INT,
                               nome VARCHAR(100),
                               password VARCHAR(100),
                               email VARCHAR(100),
                               telefone VARCHAR(20)
);