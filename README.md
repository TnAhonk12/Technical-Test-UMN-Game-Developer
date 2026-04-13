# Technical-Test-UMN-Game-Developer

## How to Play

### Controls

* **Left Click (Empty Area)** -> Spawn makanan
* **Left Click (Fish)** -> Ikan akan kabur
* **Left Click (Trash)** -> Menghapus sampah
* **ESC** -> Pause Menu

---

### Gameplay Mechanics

* Ikan memiliki **Hunger System**

  * 0 -> mencari makanan
  * 100 -> kenyang
* Ikan akan:

  * Berenang random
  * Mencari makanan saat lapar
  * Kabur saat disentuh
* Sampah akan:

  * Floating di area akuarium
* Player dapat:

  * Memberi makan (max batch system (Default = 7))
  * Membersihkan sampah

---

## Configuration System

Game menggunakan file konfigurasi eksternal:

```text
config.json
```

Berisi:

* Speed ikan & sampah
* Size object
* Hunger system
* Config spesifik tiap jenis (ANGELFISH, COD, dll)

Tujuan:

* Mengubah gameplay tanpa rebuild game

---

## File-Based Spawning System

Game membaca aset dari folder:

```text
AquascapeAssets/
```

Format penamaan:

```text
FISH_<TYPE>_<YYYYMMDDHHMMSS>.png
TRASH_<TYPE>_<YYYYMMDDHHMMSS>.png
```

Contoh:

```text
FISH_ANGELFISH_20260413201530.png
TRASH_LOG_20260413201610.png
```

Sistem akan:

* Scan folder
* Load image runtime
* Spawn object otomatis

---

## System Architecture

### Core Systems:

* **GameManager** -> Player interaction
* **Spawner** -> Object creation dari file
* **Fish (AI)** -> Behaviour & state machine
* **Trash** -> Floating behaviour
* **PauseManager** -> Game state control
* **AudioManager** -> Centralized audio system
* **ConfigManager** -> External config loader

---

## Polish & Features

* Smooth movement (DOTween)
* Visual feedback (warna ikan berdasarkan hunger)
* Audio feedback (interaction-based)
* Spawn animation (ikan dari atas)
* Pause system dengan UI & animation
* Batch-based food system + cooldown

---

## Libraries Used

* **DOTween (Demigiant)**

  * Untuk animasi (movement, UI, transition)
  * https://dotween.demigiant.com/

---

## AI Usage Disclosure

Pengembangan project ini memanfaatkan **AI sebagai alat bantu engineering**, khususnya:

Model yang digunakan:

* ChatGPT (OpenAI)

Penggunaan AI meliputi:

* Refactoring struktur kode (modular system)
* Perancangan arsitektur (Spawner, Config system, State Machine)
* Optimasi logic (batch system, input handling, pause system)
* Debugging dan problem solving
* Best practice implementation dalam Unity

Namun:

* Seluruh integrasi, pengujian, dan penyesuaian sistem dilakukan secara manual
* AI digunakan sebagai **assistant**, bukan generator penuh tanpa pemahaman

---

## Asset & Attribution

Berikut aset yang digunakan dalam project:

### Reference & Assets

* Insaniquarium Wiki
  https://insaniquarium.fandom.com/wiki/Fish_Food

* The Spriters Resource - Insaniquarium Sprites
  https://www.spriters-resource.com/pc_computer/insaniquarium/asset/25237/

* SteamGridDB
  https://www.steamgriddb.com/hero/37086

* Spriters Resource Sounds
  https://sounds.spriters-resource.com/pc_computer/insaniquarium/asset/430508/

Semua aset digunakan untuk tujuan pembelajaran dan non-komersial.

---

## Adding New Assets

Untuk menambahkan aset baru:

1. Masukkan file PNG ke folder:

```text
AquascapeAssets/
```

2. Gunakan format:

```text
FISH_<TYPE>_<ID>.png
TRASH_<TYPE>_<ID>.png
```

3. (Optional) Tambahkan config di `config.json`

Game akan otomatis membaca tanpa rebuild

---
