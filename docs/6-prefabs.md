## Prefabs
- Nome
- Descrição
- Quando são utilizados
- Quais seus componentes
    - Sprites
    - Colisores
    - Fontes de audio
    - Scripts
        - descreva o comportamento dos scripts

### Suzan (Jogadora)

- **Descrição:** Protagonista controlável pelo jogador. Possui habilidades de manipulação temporal e interação com o ambiente.
- **Quando é utilizada:** Presente em todas as fases como personagem principal.
- **Componentes:**
  - **Sprites:** Sprites de animação para caminhada e estado parado.
        ![suzan](https://github.com)
  - **Colisores:** BoxCollider2D para detectar colisões com o cenário e inimigos.
  - **Fontes de Áudio:** Passos, ataque, uso de habilidade, dano recebido.
  - **Scripts:**
    - `PlayerController.cs`: Gerencia movimento, entrada de teclado e interações básicas.
    - `TimeAbilityManager.cs`: Ativa habilidades como retroceder tempo, criar clones ou desacelerar o tempo.
    - `HealthSystem.cs`: Controla pontos de vida e dano recebido.

---

### Inimigos (Genéricos)

- **Descrição:** Criaturas temporais que vagam pelo templo. Cada tipo possui comportamento único.
- **Quando são utilizados:** Espalhados por salas de desafio, com variedade progressiva.
- **Componentes:**
  - **Sprites:** Variações visuais para tipos diferentes (sombra, caveira, guardião).
  - **Colisores:** CircleCollider2D ou BoxCollider2D para detectar impacto com Suzan.
  - **Fontes de Áudio:** Sons de ataque, morte e alerta.
  - **Scripts:**
    - `EnemyAI.cs`: Comportamento de patrulha, perseguição e ataque.
    - `HealthSystem.cs`: Gerencia vida e efeitos visuais ao morrer.
    - `DropManager.cs`: Determina se itens são deixados ao morrer.

---

### Templo Modular (Bloco de Sala)

- **Descrição:** Estrutura modular de uma sala do templo.
- **Quando é utilizado:** Cada fase é composta por múltiplos desses módulos interligados proceduralmente.
- **Componentes:**
  - **Sprites:** Piso, paredes, decoração.
  - **Colisores:** Para limitar a movimentação.
  - **Scripts:**
    - `RoomManager.cs`: Define tipo da sala (puzzle, combate, transição).
    - `EnvironmentRandomizer.cs`: Altera elementos decorativos e obstáculos conforme o tempo avança.

---

### Artefato Temporal

- **Descrição:** Itens sagrados que concedem novas habilidades temporais à Suzan.
- **Quando são utilizados:** Distribuídos em salas especiais ou após derrotar chefes.
- **Componentes:**
  - **Sprites:** Design único por artefato (Ampulheta de Ouro, Areia do Destino, etc.).
  - **Colisores:** Detecta coleta pelo jogador.
  - **Fontes de Áudio:** Som místico ao ser obtido.
  - **Scripts:**
    - `ArtifactPickup.cs`: Detecta colisão com jogador, ativa cutscene e concede habilidade.
    - `AbilityUnlocker.cs`: Libera a nova função no script `TimeAbilityManager`.

---

### Item Consumível

- **Descrição:** Itens de uso único que alteram momentaneamente o gameplay.
- **Quando são utilizados:** Obtidos em salas ou comprados com moedas temporais.
- **Componentes:**
  - **Sprites:** Poções, orbes, pergaminhos.
  - **Colisores:** Coleta automática ao contato.
  - **Fontes de Áudio:** Som de consumo ou ativação.
  - **Scripts:**
    - `ConsumableEffect.cs`: Executa o efeito desejado (ex: +1 tentativa, invulnerabilidade curta, recarga instantânea).

---

### Fragmento de Memória

- **Descrição:** Elementos colecionáveis que revelam a história do templo e da protagonista.
- **Quando são utilizados:** Espalhados em locais secretos ou fora do caminho principal.
- **Componentes:**
  - **Sprites:** Brilho suave com forma etérea.
  - **Colisores:** Trigger ao toque.
  - **Fontes de Áudio:** Sussurros e memórias.
  - **Scripts:**
    - `MemoryPickup.cs`: Armazena fragmento na coleção do jogador e desbloqueia trecho da história.
    - `LoreDisplay.cs`: Exibe o conteúdo narrativo ou visual referente ao fragmento.

