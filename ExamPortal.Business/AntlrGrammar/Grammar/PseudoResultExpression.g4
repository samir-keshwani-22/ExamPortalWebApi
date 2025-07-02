grammar PseudoResultExpression;
start: expression EOF;
expression: term (logical_operator term)*;
term:
	'(' expression ')'
	| math_expression operator math_expression;
math_expression: math_term ( (math_operator) math_term)*;

math_term:
	'(' math_expression ')'
	| 'abs(' math_expression ')'
	| '-' math_expression
	| '+' math_expression
	| IDENTIFIER;
operator: '=' | '!=' | '>' | '<' | '>=' | '<=';
logical_operator: 'AND' | 'OR';
math_operator: '+' | '-' | '/' | '*';

IDENTIFIER: [a-zA-Z0-9_]([a-zA-Z0-9_.])*;
WS: [ \t\r\n]+ -> skip;