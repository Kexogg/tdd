import "chai/register-should";
import {beginAndEndWithReporting} from "./infrastructure/reportingTest";

export default class Game {
    roll(pins) {
    }

    getScore() {
        throw new Error("Not implemented!");
    }
}

describe("Game should", () => {
    it("have zero score before any rolls", function () {
        let game = new Game();
        game.getScore().should.be.eq(0);
    });

    beginAndEndWithReporting();
});