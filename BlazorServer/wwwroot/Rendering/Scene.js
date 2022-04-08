class Scene {
    #shader
    #width
    #height
    #scale
    #camera

    constructor(width, height, scale) {
        this.#camera = {x: 0, y: 0}
        this.#shader = Shader.load(ShaderDefault.vertex, ShaderDefault.fragment);
        this.#width = width
        this.#height = height
        this.#scale = scale
        this.#shader.dimensions(width, height, scale)

        gl.enable(gl.BLEND);
        gl.blendFunc(gl.SRC_ALPHA, gl.ONE_MINUS_SRC_ALPHA)
        gl.enable(gl.DEPTH_TEST)
        gl.depthMask(gl.FALSE)
        gl.depthFunc(gl.LEQUAL)
        gl.depthRange(-1, 1)
    }

    clear() {
        gl.clearColor(0, 0, 0, 1.0)
        gl.clearDepth(1.0)
        gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT)
    }

    createActor() {
        return new Actor(this.#shader)
    }

    camera(x, y) {
        this.#camera = {x, y}
        this.#shader.camera(this.#camera.x, this.#camera.y)
    }


    dimensions(width, height, scale) {
        this.#shader.dimensions(width, height, scale)
    }
}