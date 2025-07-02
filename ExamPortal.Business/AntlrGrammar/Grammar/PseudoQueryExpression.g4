grammar PseudoQueryExpression;

start: query EOF;

query: expression | preExpression expression | aggregateQuery;

aggregateQuery:
	QUERY aggregateList FROM DATA timeFilter? whereClause?;

aggregateList: aggregate (COMMA aggregate)*;

aggregate:
	aggregateFunction LPAREN (VALUEIDENTIFIER | STAR) RPAREN;

aggregateFunction: COUNT | SUM;

timeFilter: PAST INT TIMEUNIT FROM timeReference;

timeReference:
	TRANSACTION_DATE
	| INT TIMEUNIT BEFORE TRANSACTION_DATE;

whereClause: WHERE preExpression? expression;

preExpression: IDENTIFIER IS sourceDestSpecifier AND;

sourceDestSpecifier: SOURCE_TOKEN | DEST_TOKEN;

expression: term (logicalOperator term)*;

term: LPAREN expression RPAREN | field operator value;

field: IDENTIFIER;

operator: EQ | NE | GT | LT | GE | LE | IN | NOT_IN;

value: VALUEIDENTIFIER | DATE | EMPTY;

logicalOperator: AND | OR;

QUERY: 'QUERY';

FROM: 'FROM';

DATA: 'DATA';

WHERE: 'WHERE';

AND: 'AND';

OR: 'OR';

IS: 'is';

PAST: 'PAST';

BEFORE: 'before';

COUNT: 'COUNT';

SUM: 'SUM';

IN: 'in';

NOT_IN: 'not' WS+ 'in';

TRANSACTION_DATE: 'transaction date';

SOURCE_TOKEN: '{source}';

DEST_TOKEN: '{dest}';

EQ: '=';

NE: '!=';

GT: '>';

LT: '<';

GE: '>=';

LE: '<=';

LPAREN: '(';

RPAREN: ')';

COMMA: ',';

STAR: '*';

TIMEUNIT: 'day' | 'minute' | 'hour' | 'month' | 'year';

 
DATE:
	'{' DIGIT DIGIT DIGIT DIGIT '-' DIGIT DIGIT '-' DIGIT DIGIT '}';

IDENTIFIER: '#{' IDENTIFIER_CHAR+ '}';

VALUEIDENTIFIER: '{' VALUE_CHAR+ '}';

EMPTY: '{' WS* '}';

INT: DIGIT+;

fragment DIGIT: [0-9];

fragment LETTER: [a-zA-Z];

fragment IDENTIFIER_CHAR: [a-zA-Z_] [a-zA-Z0-9_,.]*;

fragment VALUE_CHAR: ~[{}];

WS: [ \t\r\n]+ -> skip;