class Scene {
    #shader
    #width
    #height
    #scale
    #camera

    constructor() {
        this.#camera = {x: 0, y: 0}

        gl.enable(gl.BLEND);
        gl.blendFunc(gl.SRC_ALPHA, gl.ONE_MINUS_SRC_ALPHA)
        gl.enable(gl.DEPTH_TEST)
        gl.depthMask(gl.FALSE)
        gl.depthFunc(gl.LEQUAL)
        gl.depthRange(-1, 1)
    }

    static create(width, height, scale) {
        let scene = new Scene()
        scene.#shader = Shader.load(ShaderDefault.vertex, ShaderDefault.fragment);
        scene.#width = width
        scene.#height = height
        scene.#scale = scale
        scene.#shader.dimensions(width, height, scale)

        return scene
    }

    clear() {
        gl.clearColor(1.0, 0.5, 0.5, 1.0)
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