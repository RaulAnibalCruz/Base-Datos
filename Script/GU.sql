USE `5to_Bloody_Roar_2`;

create user 'administrador'@'local host' IDENTIFIED by 'passAdmin';
create user 'usuario'@'%' IDENTIFIED by 'passUsuario';

GRANT select on 5to_Bloody_Roar_2.Combate to 'usuario'@'%';
GRANT all on 5to_Bloody_Roar_2.* to 'administrador'@'local host';