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
	| IDENTIFIER
	| NUMBER;

operator: '=' | '!=' | '>' | '<' | '>=' | '<=';
logical_operator: 'AND' | 'OR';
math_operator: '+' | '-' | '/' | '*';
 
IDENTIFIER: 'Q' [0-9]+ '_' ('count' | 'sum');
 
NUMBER: [0-9]+ ('.' [0-9]+)?;

WS: [ \t\r\n]+ -> skip;

INVALID: . ;