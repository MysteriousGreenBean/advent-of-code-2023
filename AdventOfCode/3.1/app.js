import readline from 'readline';
import { readInput } from './readInput.mjs';
import { getPartNumbers } from './partNumbers.mjs';
'use strict';

const rl = readline.createInterface({
	input: process.stdin,
	output: process.stdout
});

rl.question('Provide path to input file: ', path => {
	const input = readInput(path);

	const partNumbers = getPartNumbers(input);

	console.log("Sum of part numbers: ", partNumbers.reduce((sum, value) => sum + value, 0));

	rl.question('Press Enter to exit...', () => {
		rl.close();
	});
});


