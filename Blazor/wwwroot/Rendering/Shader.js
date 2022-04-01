class Shader {
    #program
    #angle
    #position
    #color
    #index
    #camera
    #alpha
    #vertexPosition
    #size
    #dimensions

    static load(vertex, fragment) {
        let shader = new Shader()
        shader.#program = Program.load(vertex, fragment)
        gl.useProgram(shader.#program)
        shader.#angle = gl.getUniformLocation(shader.#program, "angle")
        shader.#index = gl.getUniformLocation(shader.#program, "index")
        shader.#camera = gl.getUniformLocation(shader.#program, "camera")
        shader.#position = gl.getUniformLocation(shader.#program, "position")
        shader.#alpha = gl.getUniformLocation(shader.#program, "alpha")
        shader.#color = gl.getUniformLocation(shader.#program, "color")
        shader.#size = gl.getUniformLocation(shader.#program, "size")
        shader.#vertexPosition = gl.getAttribLocation(shader.#program, "vertex_position")
        shader.#dimensions = gl.getUniformLocation(shader.#program, "dimensions")

        gl.uniform1i(shader.#index, 0)
        gl.uniform1f(shader.#alpha, 1)
        return shader
    }

    color(red, green, blue) {
        gl.uniform3f(this.#color, red, green, blue)
    }

    index(val) {
        gl.uniform1i(this.#index, val)
    }

    alpha(val = 1.0) {
        gl.uniform1f(this.#alpha, val)
    }

    position(x, y) {
        gl.uniform2f(this.#position, x, y)
    }

    camera(x, y) {
        gl.uniform2f(this.#camera, x, y)
    }

    rotate(angle) {
        gl.uniform1f(this.#angle, angle)
    }

    size(x, y) {
        gl.uniform2f(this.#size, x, y)
    }

    dimensions(width, height, scale) {
        gl.uniform3f(this.#dimensions, width, height, scale)
    }

    active() {
        gl.useProgram(this.#program)
        gl.enableVertexAttribArray(this.#vertexPosition)
        gl.vertexAttribPointer(this.#vertexPosition, 2, gl.FLOAT, false, 0, 0)
    }
}