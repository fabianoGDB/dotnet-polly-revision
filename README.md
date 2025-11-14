dotnet-polly-revision

Este projeto tem como objetivo melhorar a resiliência de aplicações .NET utilizando o Polly, permitindo lidar adequadamente com falhas temporárias e instabilidades em serviços externos.

Por que usar Polly?

Aumentar a tolerância a falhas.

Tratar falhas transitórias de rede.

Controlar chamadas a APIs externas.

Proteger o acesso a bancos de dados ou qualquer recurso instável.

Padrões de resiliência implementados
🔁 Retry (Tentativas)

Reexecuta uma operação que falhou temporariamente, esperando que o problema se resolva sozinho (ex: falha momentânea de rede).

🚧 Circuit Breaker

Padrão que interrompe automaticamente as operações quando há um número excessivo de falhas, evitando sobrecarregar um serviço que está com problemas.

Benefícios:

Impede novas tentativas enquanto o serviço está inoperante.

Evita desperdício de recursos.

Permite recuperação controlada.

Estados do Circuit Breaker

Closed
Operação funcionando normalmente. O circuito está “fechado” e todas as chamadas passam.

Open
O circuito “abre” após várias falhas. As chamadas não são enviadas ao serviço — falham imediatamente.

Half-Open
Estado de teste. O circuito permite algumas chamadas para verificar se o serviço voltou a funcionar.

Se funcionar → volta para Closed

Se falhar → volta para Open
