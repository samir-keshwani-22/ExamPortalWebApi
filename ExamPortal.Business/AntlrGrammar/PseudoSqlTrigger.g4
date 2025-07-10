grammar PseudoSqlTrigger;

start: trigger EOF;

trigger:
	expression
	| preExpression expression
	| aggregateTrigger WHERE expression
	| aggregateTrigger WHERE preExpression expression;

aggregateTrigger: QUERY aggregateList FROM DATA;

aggregateList: aggregate (COMMA aggregate)*;

aggregate: aggregateFunction LPAREN (STAR) RPAREN;

aggregateFunction: COUNT;

preExpression: ACCOUNT IS sourceDestSpecifier AND;

sourceDestSpecifier: SOURCE_TOKEN | DEST_TOKEN;

SOURCE_TOKEN: '{source}';

DEST_TOKEN: '{dest}';

expression: term (logicalOperator term)*;

term: LPAREN expression RPAREN | field operator value;

field: IDENTIFIER;

operator: EQ | NE | GT | LT | GE | LE | IN | NOT_IN;

value: VALUEIDENTIFIER | DATE;

logicalOperator: AND | OR;

QUERY: 'QUERY';
LPAREN: '(';
FROM: 'FROM';
DATA: 'DATA';
STAR: '*';
COUNT: 'COUNT';
RPAREN: ')';
WHERE: 'WHERE';
EQ: '=';
COMMA: ',';
NE: '!=';
IN: 'in';
NOT_IN: 'not' WS+ 'in';
GT: '>';
LT: '<';
GE: '>=';
IS: 'is';
LE: '<=';
AND: 'AND';

EMPTY: '{' WS* '}';

OR: 'OR';

ACCOUNT: '#{account}';

IDENTIFIER: '#{' IDENTIFIER_CHAR+ '}';

VALUEIDENTIFIER: '{' VALUE_CHAR* '}';

DATE:
	'{' DIGIT DIGIT DIGIT DIGIT '-' DIGIT DIGIT '-' DIGIT DIGIT '}';

fragment DIGIT: [0-9];
fragment IDENTIFIER_CHAR: [a-zA-Z0-9_,.];
fragment VALUE_CHAR: ['"\\@()a-zA-Z0-9:_,.];

WS: [ \t\r\n]+ -> skip;
INVALID: .;