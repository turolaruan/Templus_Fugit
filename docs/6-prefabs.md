## Prefabs

### Ampulheta

- **Descrição:** Item mágico essencial que permite ao jogador manipular o tempo.
- **Quando é utilizada:** Disponível para venda na loja.
- **Componentes:**
  - **Sprite:** `ampulheta.jpg`
        ![ampulheta](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/ampulheta.jpg)
  - **Colisor:** `None`
  - **Fonte de Áudio:** `ampulheta_pickup.mp3`
  - **Scripts:**
    - `AmpulhetaController.cs`: ativa o poder temporal e aplica adicina tempo extra.

---

### Poção de Vida

- **Descrição:** Item que restaura vida total ou parcial do jogador.
- **Quando é utilizada:** Disponível para venda na loja.
- **Componentes:**
  - **Sprite:** `pocao_vida.jpg`
        ![pocao](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/pocao_vida.jpg)
  - **Colisor:** `None`
  - **Fonte de Áudio:** `potion_drink.mp3`
  - **Scripts:**
    - `PotionPickup.cs`: restaura vida, toca som e remove o item.

---

### Fragmento de Memória

- **Descrição:** Fragmentos que revelam o passado do personagem e do vilão.
- **Quando é utilizada:** Coletável que desbloqueia segredos.
- **Componentes:**
  - **Sprite:** `fragmento_memoria.jpg`
        ![fragmento](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/fragmento_memoria.jpg)
  - **Colisor:** `BoxCollider2D`
  - **Fonte de Áudio:** `None`
  - **Scripts:**
    - `MemoryFragment.cs`: Ativa caixa de diálogo.

---

### Olho do Oráculo

- **Descrição:** Artefato que revela segredos e inimigos invisíveis.
- **Quando é utilizado:** Em áreas ocultas.
- **Componentes:**
  - **Sprite:** `olho_oraculo.jpg`
        ![fragmento](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/olho_oraculo.jpg)
  - **Colisor:** `BoxCollider2D`
  - **Fonte de Áudio:** `None`
  - **Scripts:**
    - `OracleEye.cs`: revela segredos e entra em cooldown.

---

### Marca de Cronos

- **Descrição:** Selo que distorce o tempo em determinada área.
- **Quando é utilizada:** Em puzzles e armadilhas temporais.
- **Componentes:**
  - **Sprite:** `marca_de_cronos.jpg`
        ![marca](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/marca_de_cronos.jpg)
  - **Colisor:** `PolygonCollider2D`
  - **Fonte de Áudio:** `None`
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
  - **Fonte de Áudio:** `None`
  - **Scripts:**
    - `CronosAI.cs`: controla ataques e padrões dinâmicos e muda comportamento com base na vida.

---

### Inimigo 1 (Sentinela Temporal)

- **Descrição:** Criatura que patrulha e ataca ao ver o jogador.
- **Quando é utilizado:** Em áreas com forte influência de Cronos.
- **Componentes:**
  - **Sprite:** `inimigo1.jpg`
        ![inimigo1](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/inimigo1.jpg)
  - **Colisor:** `BoxCollider2D`
  - **Fonte de Áudio:** `None`
  - **Scripts:**
    - `EnemyPatrol.cs`: patrulha e gerencia vida e efeitos de morte.

---

### Inimigo 2 (Guardião das Memórias)

- **Descrição:** Protetor dos fragmentos de memória.
- **Quando é utilizado:** Próximo a fragmentos.
- **Componentes:**
  - **Sprite:** `inimigo2.jpg`
        ![inimigo2](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/inimigo2.jpg)
  - **Colisor:** `BoxCollider2D`
  - **Fonte de Áudio:** `None`
  - **Scripts:**
    - `MemoryGuardianAI.cs`: persegue o jogador e ataca.
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
  - **Colisor:** `BoxCollider2D`
  - **Fonte de Áudio:** `None`
  - **Scripts:**
    - `Player.cs`: movimentação com animações, coleta, combate, interação com NPCs/objetos, gerencia vida, dano e morte.

---

### Vendedor da Loja

- **Descrição:** NPC que vende itens mágicos ao jogador.
- **Quando é utilizado:** Dentro da loja acessível no templo.
- **Componentes:**
  - **Sprite:** `vendedor_loja.png`
        ![vendedor](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/vendedor%20loja.jpg)
  - **Colisor:** `BoxCollider2D`
  - **Fonte de Áudio:** `None`
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
  - **Colisor:** `None` 
  - **Fonte de Áudio:** `None`
  - **Scripts:**
    - `TimeDistortionItem.cs`: o tempo para por 10 segundos.

---

### Véu de Nyx

- **Descrição:** Item passivo que torna Suzan invisível temporariamente.
- **Quando é utilizado:** Equipado após compra.
- **Componentes:**
  - **Sprite:** `veu_nyx.png`
        ![veu](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/V%C3%A9u_de_Nyx.jpg)
  - **Colisor:** `None`
  - **Fonte de Áudio:** `None`
  - **Scripts:**
    - `NyxVeilPassive.cs`: ativa invisibilidade.

---

### Baú

- **Descrição:** Baú que pode conter poções, fragmentos ou itens raros.
- **Quando é utilizado:** Espalhado pelo mapa como recompensa de exploração.
- **Componentes:**
  - **Sprite:** `bau_fechado.png`
        ![bau](https://github.com/LucasRezendeSimoes/Templus_Fugit/blob/main/project/images/bau.jpg)
  - **Colisor:** `BoxCollider2D`
  - **Fonte de Áudio:** `chest_open.mp3`
  - **Scripts:**
    - `ChestController.cs`: desafio para abrir o baú, sorteia recompensa.
