class Actor {
    #buffer
    #bufferSize
    #shader
    #position
    #index
    #alpha
    #angle
    #size
    #color

    constructor(shader) {
        this.#buffer = gl.createBuffer()
        this.#bufferSize = 0
        this.#shader = shader
        this.#position = {x: 0, y: 0}
        this.#index = 0
        this.#alpha = 1.0
        this.#angle = 0
        this.#size = {x: 1.0, y: 1.0}
        this.#color = {r: 0, g: 0, b: 0}
    }

    uploadData(data) {
        var f32data = new Float32Array(data)
        gl.bindBuffer(gl.ARRAY_BUFFER, this.#buffer)
        gl.bufferData(gl.ARRAY_BUFFER, f32data, gl.STATIC_DRAW)
        this.#bufferSize = f32data.length / 2
    }

    position(x, y) {
        this.#position = {x, y}
    }

    rotation(degrees) {
        this.#angle = degrees
    }

    transparency(alpha = 1) {
        this.#alpha = alpha
    }

    index(index = 0) {
        this.#index = index
    }

    scale(x, y) {
        this.#size = {x, y}
    }

    color(r, g, b) {
        this.#color = {r, g, b}
    }

    draw() {
        gl.bindBuffer(gl.ARRAY_BUFFER, this.#buffer)
        this.#shader.active()
        this.#shader.position(this.#position.x, this.#position.y)
        this.#shader.rotate(this.#angle)
        this.#shader.alpha(this.#alpha)
        this.#shader.index(this.#index)
        this.#shader.size(this.#size.x, this.#size.y)
        this.#shader.color(this.#color.r / 255, this.#color.g / 255, this.#color.b / 255)
        gl.drawArrays(gl.TRIANGLES, 0, this.#bufferSize)
    }
}