const Application = function () {
    this.initA4();
    this.tuner = new Tuner(this.a4);
    this.notes = new Notes(".notes", this.tuner);
    this.update({
        name: "A",
        frequency: this.a4,
        octave: 4,
        value: 69,
        cents: 0,
    });
};

Application.prototype.initA4 = function () {
    this.$a4 = document.querySelector(".a4 span");
    this.a4 = parseInt(localStorage.getItem("a4")) || 440;
    this.$a4.innerHTML = this.a4;
};

Application.prototype.start = function () {
    const self = this;

    this.tuner.onNoteDetected = function (note) {
        if (self.notes.isAutoMode) {
            if (self.lastNote === note.name) {
                self.update(note);
            } else {
                self.lastNote = note.name;
            }
        }
    };

    this.$a4.addEventListener("click", function () {
        self.tuner.init();
        self.frequencyData = new Uint8Array(self.tuner.analyser.frequencyBinCount);
    });

    document.querySelector(".auto input").addEventListener("change", () => {
        this.notes.toggleAutoMode();
    });

    instrumentoSelect.addEventListener("change", () => {
        if (instrumentoSelect.value === 'Bateria') {
            self.tuner.init();
            self.frequencyData = new Uint8Array(self.tuner.analyser.frequencyBinCount);
        }
    });
};


Application.prototype.update = function (note) {
    var nota = note.name;
    notaTuner.innerText = nota;
    try {
        _instrumentoSelecionado = 'strings';
        verificarAcompanhamentoEtocar(nota);
    } catch { }
};

const app = new Application();
app.start();
