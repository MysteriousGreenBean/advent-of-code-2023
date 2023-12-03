import readline from 'readline';
import { getGearRatios } from './gearRatios.mjs';
import { readInput } from './readInput.mjs';
'use strict';

const rl = readline.createInterface({
	input: process.stdin,
	output: process.stdout
});

rl.question('Provide path to input file: ', path => {
	const input = readInput(path);

	const gearRatios = getGearRatios(input);

	console.log("Sum of gear ratios: ", gearRatios.reduce((sum, ratio) => sum + ratio, 0));

	rl.question('Press Enter to exit...', () => {
		rl.close();
	});
});


