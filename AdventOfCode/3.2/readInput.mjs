'use strict';
import fs from 'fs';

const readInput = (path) => {
	if (fs.existsSync(path)) {
		const data = fs.readFileSync(path, 'utf8');
		return data;
	}
	else {
		console.error(`File ${path} does not exist`);
	}
}

export { readInput };
