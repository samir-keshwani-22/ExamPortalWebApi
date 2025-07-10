// Generated from d:/ExamPortalWebApi/ExamPortal.Business/AntlrGrammar/PseudoSqlTrigger.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast", "CheckReturnValue"})
public class PseudoSqlTriggerParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.13.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		SOURCE_TOKEN=1, DEST_TOKEN=2, QUERY=3, LPAREN=4, FROM=5, DATA=6, STAR=7, 
		COUNT=8, RPAREN=9, WHERE=10, EQ=11, COMMA=12, NE=13, IN=14, NOT_IN=15, 
		GT=16, LT=17, GE=18, IS=19, LE=20, AND=21, EMPTY=22, OR=23, ACCOUNT=24, 
		IDENTIFIER=25, VALUEIDENTIFIER=26, DATE=27, WS=28, INVALID=29;
	public static final int
		RULE_start = 0, RULE_trigger = 1, RULE_aggregateTrigger = 2, RULE_aggregateList = 3, 
		RULE_aggregate = 4, RULE_aggregateFunction = 5, RULE_preExpression = 6, 
		RULE_sourceDestSpecifier = 7, RULE_expression = 8, RULE_term = 9, RULE_field = 10, 
		RULE_operator = 11, RULE_value = 12, RULE_logicalOperator = 13;
	private static String[] makeRuleNames() {
		return new String[] {
			"start", "trigger", "aggregateTrigger", "aggregateList", "aggregate", 
			"aggregateFunction", "preExpression", "sourceDestSpecifier", "expression", 
			"term", "field", "operator", "value", "logicalOperator"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'{source}'", "'{dest}'", "'QUERY'", "'('", "'FROM'", "'DATA'", 
			"'*'", "'COUNT'", "')'", "'WHERE'", "'='", "','", "'!='", "'in'", null, 
			"'>'", "'<'", "'>='", "'is'", "'<='", "'AND'", null, "'OR'", "'#{account}'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "SOURCE_TOKEN", "DEST_TOKEN", "QUERY", "LPAREN", "FROM", "DATA", 
			"STAR", "COUNT", "RPAREN", "WHERE", "EQ", "COMMA", "NE", "IN", "NOT_IN", 
			"GT", "LT", "GE", "IS", "LE", "AND", "EMPTY", "OR", "ACCOUNT", "IDENTIFIER", 
			"VALUEIDENTIFIER", "DATE", "WS", "INVALID"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "PseudoSqlTrigger.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public PseudoSqlTriggerParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@SuppressWarnings("CheckReturnValue")
	public static class StartContext extends ParserRuleContext {
		public TriggerContext trigger() {
			return getRuleContext(TriggerContext.class,0);
		}
		public TerminalNode EOF() { return getToken(PseudoSqlTriggerParser.EOF, 0); }
		public StartContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_start; }
	}

	public final StartContext start() throws RecognitionException {
		StartContext _localctx = new StartContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_start);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(28);
			trigger();
			setState(29);
			match(EOF);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class TriggerContext extends ParserRuleContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public PreExpressionContext preExpression() {
			return getRuleContext(PreExpressionContext.class,0);
		}
		public AggregateTriggerContext aggregateTrigger() {
			return getRuleContext(AggregateTriggerContext.class,0);
		}
		public TerminalNode WHERE() { return getToken(PseudoSqlTriggerParser.WHERE, 0); }
		public TriggerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_trigger; }
	}

	public final TriggerContext trigger() throws RecognitionException {
		TriggerContext _localctx = new TriggerContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_trigger);
		try {
			setState(44);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,0,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(31);
				expression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(32);
				preExpression();
				setState(33);
				expression();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(35);
				aggregateTrigger();
				setState(36);
				match(WHERE);
				setState(37);
				expression();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(39);
				aggregateTrigger();
				setState(40);
				match(WHERE);
				setState(41);
				preExpression();
				setState(42);
				expression();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class AggregateTriggerContext extends ParserRuleContext {
		public TerminalNode QUERY() { return getToken(PseudoSqlTriggerParser.QUERY, 0); }
		public AggregateListContext aggregateList() {
			return getRuleContext(AggregateListContext.class,0);
		}
		public TerminalNode FROM() { return getToken(PseudoSqlTriggerParser.FROM, 0); }
		public TerminalNode DATA() { return getToken(PseudoSqlTriggerParser.DATA, 0); }
		public AggregateTriggerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_aggregateTrigger; }
	}

	public final AggregateTriggerContext aggregateTrigger() throws RecognitionException {
		AggregateTriggerContext _localctx = new AggregateTriggerContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_aggregateTrigger);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(46);
			match(QUERY);
			setState(47);
			aggregateList();
			setState(48);
			match(FROM);
			setState(49);
			match(DATA);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class AggregateListContext extends ParserRuleContext {
		public List<AggregateContext> aggregate() {
			return getRuleContexts(AggregateContext.class);
		}
		public AggregateContext aggregate(int i) {
			return getRuleContext(AggregateContext.class,i);
		}
		public List<TerminalNode> COMMA() { return getTokens(PseudoSqlTriggerParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(PseudoSqlTriggerParser.COMMA, i);
		}
		public AggregateListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_aggregateList; }
	}

	public final AggregateListContext aggregateList() throws RecognitionException {
		AggregateListContext _localctx = new AggregateListContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_aggregateList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(51);
			aggregate();
			setState(56);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(52);
				match(COMMA);
				setState(53);
				aggregate();
				}
				}
				setState(58);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class AggregateContext extends ParserRuleContext {
		public AggregateFunctionContext aggregateFunction() {
			return getRuleContext(AggregateFunctionContext.class,0);
		}
		public TerminalNode LPAREN() { return getToken(PseudoSqlTriggerParser.LPAREN, 0); }
		public TerminalNode RPAREN() { return getToken(PseudoSqlTriggerParser.RPAREN, 0); }
		public TerminalNode STAR() { return getToken(PseudoSqlTriggerParser.STAR, 0); }
		public AggregateContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_aggregate; }
	}

	public final AggregateContext aggregate() throws RecognitionException {
		AggregateContext _localctx = new AggregateContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_aggregate);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(59);
			aggregateFunction();
			setState(60);
			match(LPAREN);
			{
			setState(61);
			match(STAR);
			}
			setState(62);
			match(RPAREN);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class AggregateFunctionContext extends ParserRuleContext {
		public TerminalNode COUNT() { return getToken(PseudoSqlTriggerParser.COUNT, 0); }
		public AggregateFunctionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_aggregateFunction; }
	}

	public final AggregateFunctionContext aggregateFunction() throws RecognitionException {
		AggregateFunctionContext _localctx = new AggregateFunctionContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_aggregateFunction);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(64);
			match(COUNT);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class PreExpressionContext extends ParserRuleContext {
		public TerminalNode ACCOUNT() { return getToken(PseudoSqlTriggerParser.ACCOUNT, 0); }
		public TerminalNode IS() { return getToken(PseudoSqlTriggerParser.IS, 0); }
		public SourceDestSpecifierContext sourceDestSpecifier() {
			return getRuleContext(SourceDestSpecifierContext.class,0);
		}
		public TerminalNode AND() { return getToken(PseudoSqlTriggerParser.AND, 0); }
		public PreExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_preExpression; }
	}

	public final PreExpressionContext preExpression() throws RecognitionException {
		PreExpressionContext _localctx = new PreExpressionContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_preExpression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(66);
			match(ACCOUNT);
			setState(67);
			match(IS);
			setState(68);
			sourceDestSpecifier();
			setState(69);
			match(AND);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class SourceDestSpecifierContext extends ParserRuleContext {
		public TerminalNode SOURCE_TOKEN() { return getToken(PseudoSqlTriggerParser.SOURCE_TOKEN, 0); }
		public TerminalNode DEST_TOKEN() { return getToken(PseudoSqlTriggerParser.DEST_TOKEN, 0); }
		public SourceDestSpecifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_sourceDestSpecifier; }
	}

	public final SourceDestSpecifierContext sourceDestSpecifier() throws RecognitionException {
		SourceDestSpecifierContext _localctx = new SourceDestSpecifierContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_sourceDestSpecifier);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(71);
			_la = _input.LA(1);
			if ( !(_la==SOURCE_TOKEN || _la==DEST_TOKEN) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ExpressionContext extends ParserRuleContext {
		public List<TermContext> term() {
			return getRuleContexts(TermContext.class);
		}
		public TermContext term(int i) {
			return getRuleContext(TermContext.class,i);
		}
		public List<LogicalOperatorContext> logicalOperator() {
			return getRuleContexts(LogicalOperatorContext.class);
		}
		public LogicalOperatorContext logicalOperator(int i) {
			return getRuleContext(LogicalOperatorContext.class,i);
		}
		public ExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression; }
	}

	public final ExpressionContext expression() throws RecognitionException {
		ExpressionContext _localctx = new ExpressionContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(73);
			term();
			setState(79);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==AND || _la==OR) {
				{
				{
				setState(74);
				logicalOperator();
				setState(75);
				term();
				}
				}
				setState(81);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class TermContext extends ParserRuleContext {
		public TerminalNode LPAREN() { return getToken(PseudoSqlTriggerParser.LPAREN, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode RPAREN() { return getToken(PseudoSqlTriggerParser.RPAREN, 0); }
		public FieldContext field() {
			return getRuleContext(FieldContext.class,0);
		}
		public OperatorContext operator() {
			return getRuleContext(OperatorContext.class,0);
		}
		public ValueContext value() {
			return getRuleContext(ValueContext.class,0);
		}
		public TermContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_term; }
	}

	public final TermContext term() throws RecognitionException {
		TermContext _localctx = new TermContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_term);
		try {
			setState(90);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case LPAREN:
				enterOuterAlt(_localctx, 1);
				{
				setState(82);
				match(LPAREN);
				setState(83);
				expression();
				setState(84);
				match(RPAREN);
				}
				break;
			case IDENTIFIER:
				enterOuterAlt(_localctx, 2);
				{
				setState(86);
				field();
				setState(87);
				operator();
				setState(88);
				value();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class FieldContext extends ParserRuleContext {
		public TerminalNode IDENTIFIER() { return getToken(PseudoSqlTriggerParser.IDENTIFIER, 0); }
		public FieldContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_field; }
	}

	public final FieldContext field() throws RecognitionException {
		FieldContext _localctx = new FieldContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_field);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(92);
			match(IDENTIFIER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class OperatorContext extends ParserRuleContext {
		public TerminalNode EQ() { return getToken(PseudoSqlTriggerParser.EQ, 0); }
		public TerminalNode NE() { return getToken(PseudoSqlTriggerParser.NE, 0); }
		public TerminalNode GT() { return getToken(PseudoSqlTriggerParser.GT, 0); }
		public TerminalNode LT() { return getToken(PseudoSqlTriggerParser.LT, 0); }
		public TerminalNode GE() { return getToken(PseudoSqlTriggerParser.GE, 0); }
		public TerminalNode LE() { return getToken(PseudoSqlTriggerParser.LE, 0); }
		public TerminalNode IN() { return getToken(PseudoSqlTriggerParser.IN, 0); }
		public TerminalNode NOT_IN() { return getToken(PseudoSqlTriggerParser.NOT_IN, 0); }
		public OperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_operator; }
	}

	public final OperatorContext operator() throws RecognitionException {
		OperatorContext _localctx = new OperatorContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(94);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 1566720L) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ValueContext extends ParserRuleContext {
		public TerminalNode VALUEIDENTIFIER() { return getToken(PseudoSqlTriggerParser.VALUEIDENTIFIER, 0); }
		public TerminalNode DATE() { return getToken(PseudoSqlTriggerParser.DATE, 0); }
		public ValueContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_value; }
	}

	public final ValueContext value() throws RecognitionException {
		ValueContext _localctx = new ValueContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_value);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(96);
			_la = _input.LA(1);
			if ( !(_la==VALUEIDENTIFIER || _la==DATE) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class LogicalOperatorContext extends ParserRuleContext {
		public TerminalNode AND() { return getToken(PseudoSqlTriggerParser.AND, 0); }
		public TerminalNode OR() { return getToken(PseudoSqlTriggerParser.OR, 0); }
		public LogicalOperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_logicalOperator; }
	}

	public final LogicalOperatorContext logicalOperator() throws RecognitionException {
		LogicalOperatorContext _localctx = new LogicalOperatorContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_logicalOperator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(98);
			_la = _input.LA(1);
			if ( !(_la==AND || _la==OR) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static final String _serializedATN =
		"\u0004\u0001\u001de\u0002\u0000\u0007\u0000\u0002\u0001\u0007\u0001\u0002"+
		"\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002\u0004\u0007\u0004\u0002"+
		"\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002\u0007\u0007\u0007\u0002"+
		"\b\u0007\b\u0002\t\u0007\t\u0002\n\u0007\n\u0002\u000b\u0007\u000b\u0002"+
		"\f\u0007\f\u0002\r\u0007\r\u0001\u0000\u0001\u0000\u0001\u0000\u0001\u0001"+
		"\u0001\u0001\u0001\u0001\u0001\u0001\u0001\u0001\u0001\u0001\u0001\u0001"+
		"\u0001\u0001\u0001\u0001\u0001\u0001\u0001\u0001\u0001\u0001\u0001\u0001"+
		"\u0003\u0001-\b\u0001\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002"+
		"\u0001\u0002\u0001\u0003\u0001\u0003\u0001\u0003\u0005\u00037\b\u0003"+
		"\n\u0003\f\u0003:\t\u0003\u0001\u0004\u0001\u0004\u0001\u0004\u0001\u0004"+
		"\u0001\u0004\u0001\u0005\u0001\u0005\u0001\u0006\u0001\u0006\u0001\u0006"+
		"\u0001\u0006\u0001\u0006\u0001\u0007\u0001\u0007\u0001\b\u0001\b\u0001"+
		"\b\u0001\b\u0005\bN\b\b\n\b\f\bQ\t\b\u0001\t\u0001\t\u0001\t\u0001\t\u0001"+
		"\t\u0001\t\u0001\t\u0001\t\u0003\t[\b\t\u0001\n\u0001\n\u0001\u000b\u0001"+
		"\u000b\u0001\f\u0001\f\u0001\r\u0001\r\u0001\r\u0000\u0000\u000e\u0000"+
		"\u0002\u0004\u0006\b\n\f\u000e\u0010\u0012\u0014\u0016\u0018\u001a\u0000"+
		"\u0004\u0001\u0000\u0001\u0002\u0003\u0000\u000b\u000b\r\u0012\u0014\u0014"+
		"\u0001\u0000\u001a\u001b\u0002\u0000\u0015\u0015\u0017\u0017\\\u0000\u001c"+
		"\u0001\u0000\u0000\u0000\u0002,\u0001\u0000\u0000\u0000\u0004.\u0001\u0000"+
		"\u0000\u0000\u00063\u0001\u0000\u0000\u0000\b;\u0001\u0000\u0000\u0000"+
		"\n@\u0001\u0000\u0000\u0000\fB\u0001\u0000\u0000\u0000\u000eG\u0001\u0000"+
		"\u0000\u0000\u0010I\u0001\u0000\u0000\u0000\u0012Z\u0001\u0000\u0000\u0000"+
		"\u0014\\\u0001\u0000\u0000\u0000\u0016^\u0001\u0000\u0000\u0000\u0018"+
		"`\u0001\u0000\u0000\u0000\u001ab\u0001\u0000\u0000\u0000\u001c\u001d\u0003"+
		"\u0002\u0001\u0000\u001d\u001e\u0005\u0000\u0000\u0001\u001e\u0001\u0001"+
		"\u0000\u0000\u0000\u001f-\u0003\u0010\b\u0000 !\u0003\f\u0006\u0000!\""+
		"\u0003\u0010\b\u0000\"-\u0001\u0000\u0000\u0000#$\u0003\u0004\u0002\u0000"+
		"$%\u0005\n\u0000\u0000%&\u0003\u0010\b\u0000&-\u0001\u0000\u0000\u0000"+
		"\'(\u0003\u0004\u0002\u0000()\u0005\n\u0000\u0000)*\u0003\f\u0006\u0000"+
		"*+\u0003\u0010\b\u0000+-\u0001\u0000\u0000\u0000,\u001f\u0001\u0000\u0000"+
		"\u0000, \u0001\u0000\u0000\u0000,#\u0001\u0000\u0000\u0000,\'\u0001\u0000"+
		"\u0000\u0000-\u0003\u0001\u0000\u0000\u0000./\u0005\u0003\u0000\u0000"+
		"/0\u0003\u0006\u0003\u000001\u0005\u0005\u0000\u000012\u0005\u0006\u0000"+
		"\u00002\u0005\u0001\u0000\u0000\u000038\u0003\b\u0004\u000045\u0005\f"+
		"\u0000\u000057\u0003\b\u0004\u000064\u0001\u0000\u0000\u00007:\u0001\u0000"+
		"\u0000\u000086\u0001\u0000\u0000\u000089\u0001\u0000\u0000\u00009\u0007"+
		"\u0001\u0000\u0000\u0000:8\u0001\u0000\u0000\u0000;<\u0003\n\u0005\u0000"+
		"<=\u0005\u0004\u0000\u0000=>\u0005\u0007\u0000\u0000>?\u0005\t\u0000\u0000"+
		"?\t\u0001\u0000\u0000\u0000@A\u0005\b\u0000\u0000A\u000b\u0001\u0000\u0000"+
		"\u0000BC\u0005\u0018\u0000\u0000CD\u0005\u0013\u0000\u0000DE\u0003\u000e"+
		"\u0007\u0000EF\u0005\u0015\u0000\u0000F\r\u0001\u0000\u0000\u0000GH\u0007"+
		"\u0000\u0000\u0000H\u000f\u0001\u0000\u0000\u0000IO\u0003\u0012\t\u0000"+
		"JK\u0003\u001a\r\u0000KL\u0003\u0012\t\u0000LN\u0001\u0000\u0000\u0000"+
		"MJ\u0001\u0000\u0000\u0000NQ\u0001\u0000\u0000\u0000OM\u0001\u0000\u0000"+
		"\u0000OP\u0001\u0000\u0000\u0000P\u0011\u0001\u0000\u0000\u0000QO\u0001"+
		"\u0000\u0000\u0000RS\u0005\u0004\u0000\u0000ST\u0003\u0010\b\u0000TU\u0005"+
		"\t\u0000\u0000U[\u0001\u0000\u0000\u0000VW\u0003\u0014\n\u0000WX\u0003"+
		"\u0016\u000b\u0000XY\u0003\u0018\f\u0000Y[\u0001\u0000\u0000\u0000ZR\u0001"+
		"\u0000\u0000\u0000ZV\u0001\u0000\u0000\u0000[\u0013\u0001\u0000\u0000"+
		"\u0000\\]\u0005\u0019\u0000\u0000]\u0015\u0001\u0000\u0000\u0000^_\u0007"+
		"\u0001\u0000\u0000_\u0017\u0001\u0000\u0000\u0000`a\u0007\u0002\u0000"+
		"\u0000a\u0019\u0001\u0000\u0000\u0000bc\u0007\u0003\u0000\u0000c\u001b"+
		"\u0001\u0000\u0000\u0000\u0004,8OZ";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}