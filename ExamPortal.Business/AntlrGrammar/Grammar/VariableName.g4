grammar VariableName;

start: variableList EOF;

variableList: variable (COMMA variable)?;

variable: Q_PREFIX INT UNDERSCORE aggregateType;

aggregateType: COUNT | SUM;

Q_PREFIX: 'Q';
INT: DIGIT+;
UNDERSCORE: '_';
COUNT: 'count';
SUM: 'sum';
COMMA: ',';

fragment DIGIT: [0-9];

WS: [ \t\r\n]+ -> skip;
INVALID: .;