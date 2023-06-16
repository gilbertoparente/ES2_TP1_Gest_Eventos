CREATE TABLE inscricoes (
                            id SERIAL PRIMARY KEY,
                            evento_id INT REFERENCES eventos(id),
                            participante_id INT REFERENCES participantes(id),
                            data_inscricao TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
