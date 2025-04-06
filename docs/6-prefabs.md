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

### Ampulheta

- **Descrição:** Item mágico essencial que permite ao jogador manipular o tempo.
- **Quando é utilizada:** Espalhada no mapa e obtida após desafios.
- **Componentes:**
  - **Sprite:** `ampulheta.jpg`
        ![ampulheta](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/ampulheta.jpg)
  - **Colisor:** `BoxCollider2D`
  - **Fonte de Áudio:** `ampulheta_pickup.wav`
  - **Scripts:**
    - `AmpulhetaController.cs`: ativa o poder temporal, aplica efeitos e remove o item da cena.

---

### Poção de Vida

- **Descrição:** Item que restaura vida total ou parcial do jogador.
- **Quando é utilizada:** Após combates ou escondida no mapa.
- **Componentes:**
  - **Sprite:** `pocao_vida.jpg`
        ![pocao](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/pocao_vida.jpg)
  - **Colisor:** `CircleCollider2D`
  - **Fonte de Áudio:** `potion_drink.wav`
  - **Scripts:**
    - `PotionPickup.cs`: restaura vida, toca som e remove o item.

---

### Fragmento de Memória

- **Descrição:** Fragmentos que revelam o passado do personagem e do vilão.
- **Quando é utilizada:** Coletável que desbloqueia cutscenes ou segredos.
- **Componentes:**
  - **Sprite:** `fragmento_memoria.jpg`
        ![fragmento](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/fragmento_memoria.jpg)
  - **Colisor:** `BoxCollider2D`
  - **Fonte de Áudio:** `memory_fragment.wav`
  - **Scripts:**
    - `MemoryFragment.cs`: salva progresso, ativa cutscene e atualiza UI.

---

### Olho do Oráculo

- **Descrição:** Artefato que revela segredos e inimigos invisíveis.
- **Quando é utilizado:** Em áreas ocultas ou eventos críticos.
- **Componentes:**
  - **Sprite:** `olho_oraculo.jpg`
        ![fragmento](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/olho_oraculo.jpg)
  - **Colisor:** `BoxCollider2D` (`IsTrigger`)
  - **Fonte de Áudio:** `oracle_eye.wav`
  - **Scripts:**
    - `OracleEye.cs`: revela segredos, muda ambiente e entra em cooldown.

---

### Marca de Cronos

- **Descrição:** Selo que distorce o tempo em determinada área.
- **Quando é utilizada:** Em puzzles e armadilhas temporais.
- **Componentes:**
  - **Sprite:** `marca_de_cronos.jpg`
        ![marca](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/marca_de_cronos.jpg)
  - **Colisor:** `PolygonCollider2D`
  - **Fonte de Áudio:** `corruption.wav`
  - **Scripts:**
    - `CronosMark.cs`: altera regras do tempo e pode causar debuffs.

---

### Cronos (vida cheia / meia vida / vida baixa)

- **Descrição:** Vilão principal do jogo, com fases de comportamento.
- **Quando é utilizado:** Em boss fights ao longo da campanha.
- **Componentes:**
  - **Sprites:** `cronos_fullvida.jpg`, `cronos_metadevida.jpg`, `cronos_vidabaixa.jpg`
    - Vida Completa
        ![cronosfullvida](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/cronos_fullvida.jpg)
    - Metade da vida
        ![cronosmetadevida](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/cronos_metadevida.jpg)
    - Vida baixa 
        ![cronosvidabaixa](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/cronos_vidabaixa.jpg)
  - **Colisor:** `BoxCollider2D` + `Rigidbody2D`
  - **Fonte de Áudio:** `cronos_theme.wav`
  - **Scripts:**
    - `CronosAI.cs`: controla ataques e padrões dinâmicos.
    - `BossHealthManager.cs`: muda comportamento com base na vida.

---

### Inimigo 1 (Sentinela Temporal)

- **Descrição:** Criatura que patrulha e ataca ao ver o jogador.
- **Quando é utilizado:** Em áreas com forte influência de Cronos.
- **Componentes:**
  - **Sprite:** `inimigo1.jpg`
        ![inimigo1](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/inimigo1.jpg)
  - **Colisor:** `CircleCollider2D`
  - **Fonte de Áudio:** `sentinel_alert.wav`
  - **Scripts:**
    - `EnemyPatrol.cs`: patrulha e ataca com energia temporal.
    - `EnemyHealth.cs`: gerencia vida e efeitos de morte.

---

### Inimigo 2 (Guardião das Memórias)

- **Descrição:** Protetor dos fragmentos de memória.
- **Quando é utilizado:** Próximo a fragmentos.
- **Componentes:**
  - **Sprite:** `inimigo2.jpg`
        ![inimigo2](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/inimigo2.jpg)
  - **Colisor:** `BoxCollider2D`
  - **Fonte de Áudio:** `memory_guard.wav`
  - **Scripts:**
    - `MemoryGuardianAI.cs`: persegue o jogador e ataca em área.
    - `GuardianHealth.cs`: libera fragmento após derrota.

---

### Suzan (Personagem Jogável)

- **Descrição:** Protagonista controlada pelo jogador.
- **Quando é utilizada:** Sempre durante a exploração do templo.
- **Componentes:**
  - **Sprites:** Suzan parada (`suzan_idle.png`), Suzan andando (`suzan_walk1.png`, `suzan_walk2.png`)
      - Parada
        ![suzanparada](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/suzan3.jpg)
      - Andando
        ![suzanandando1](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/suzan1.jpg)
        ![suzanandando2](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/suzan2.jpg)
  - **Colisor:** `CapsuleCollider2D`
  - **Fonte de Áudio:** `footsteps.wav`, `damage.wav`
  - **Scripts:**
    - `PlayerMovement.cs`: movimentação com animações.
    - `PlayerInteraction.cs`: coleta, combate e interação com NPCs/objetos.
    - `HealthSystem.cs`: gerencia vida, dano e morte.

---

### Vendedor da Loja

- **Descrição:** NPC que vende itens mágicos ao jogador.
- **Quando é utilizado:** Dentro da loja acessível no templo.
- **Componentes:**
  - **Sprite:** `vendedor_loja.png`
        ![vendedor](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/vendedor%20loja.jpg)
  - **Colisor:** `BoxCollider2D`
  - **Fonte de Áudio:** `shopkeeper_greeting.wav`
  - **Scripts:**
    - `ShopkeeperDialogue.cs`: exibe diálogos e opções de compra.
    - `ShopInterface.cs`: ativa UI da loja e manipula inventário.

---

### Relógio Quebrado

- **Descrição:** Item consumível que desacelera o tempo por alguns segundos.
- **Quando é utilizado:** Durante combates ou puzzles que exigem rapidez.
- **Componentes:**
  - **Sprite:** `relogio_quebrado.png`
        ![relogio](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/relogio_quebrado.jpg)
  - **Colisor:** `None` (UI/Inventário)
  - **Fonte de Áudio:** `broken_clock.wav`
  - **Scripts:**
    - `TimeDistortionItem.cs`: aplica efeito de lentidão global por tempo limitado.

---

### Véu de Nyx

- **Descrição:** Item passivo que torna Suzan invisível temporariamente ao usar a Ampulheta.
- **Quando é utilizado:** Equipado após compra; ativado junto à habilidade principal.
- **Componentes:**
  - **Sprite:** `veu_nyx.png`
        ![veu](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/V%C3%A9u_de_Nyx.jpg)
  - **Colisor:** `None` (UI/Inventário)
  - **Fonte de Áudio:** `veil_activate.wav`
  - **Scripts:**
    - `NyxVeilPassive.cs`: ativa invisibilidade quando a manipulação do tempo está ativa.

---

### Baú

- **Descrição:** Baú que pode conter poções, fragmentos ou itens raros.
- **Quando é utilizado:** Espalhado pelo mapa como recompensa de exploração.
- **Componentes:**
  - **Sprite:** `bau_fechado.png`, `bau_aberto.png`
  - **Colisor:** `BoxCollider2D`
  - **Fonte de Áudio:** `chest_open.wav`
  - **Scripts:**
    - `ChestController.cs`: abre o baú, sorteia recompensa e atualiza o sprite.
