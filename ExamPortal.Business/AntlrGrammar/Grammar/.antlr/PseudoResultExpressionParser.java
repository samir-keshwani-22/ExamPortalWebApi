// Generated from d:/ExamPortalWebApi/ExamPortal.Business/AntlrGrammar/Grammar/PseudoResultExpression.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast", "CheckReturnValue"})
public class PseudoResultExpressionParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.13.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, IDENTIFIER=16, 
		NUMBER=17, WS=18, INVALID=19;
	public static final int
		RULE_start = 0, RULE_expression = 1, RULE_term = 2, RULE_math_expression = 3, 
		RULE_math_term = 4, RULE_operator = 5, RULE_logical_operator = 6, RULE_math_operator = 7;
	private static String[] makeRuleNames() {
		return new String[] {
			"start", "expression", "term", "math_expression", "math_term", "operator", 
			"logical_operator", "math_operator"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'('", "')'", "'abs('", "'-'", "'+'", "'='", "'!='", "'>'", "'<'", 
			"'>='", "'<='", "'AND'", "'OR'", "'/'", "'*'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, "IDENTIFIER", "NUMBER", "WS", "INVALID"
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
	public String getGrammarFileName() { return "PseudoResultExpression.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public PseudoResultExpressionParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@SuppressWarnings("CheckReturnValue")
	public static class StartContext extends ParserRuleContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode EOF() { return getToken(PseudoResultExpressionParser.EOF, 0); }
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
			setState(16);
			expression();
			setState(17);
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
	public static class ExpressionContext extends ParserRuleContext {
		public List<TermContext> term() {
			return getRuleContexts(TermContext.class);
		}
		public TermContext term(int i) {
			return getRuleContext(TermContext.class,i);
		}
		public List<Logical_operatorContext> logical_operator() {
			return getRuleContexts(Logical_operatorContext.class);
		}
		public Logical_operatorContext logical_operator(int i) {
			return getRuleContext(Logical_operatorContext.class,i);
		}
		public ExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression; }
	}

	public final ExpressionContext expression() throws RecognitionException {
		ExpressionContext _localctx = new ExpressionContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(19);
			term();
			setState(25);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__11 || _la==T__12) {
				{
				{
				setState(20);
				logical_operator();
				setState(21);
				term();
				}
				}
				setState(27);
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
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public List<Math_expressionContext> math_expression() {
			return getRuleContexts(Math_expressionContext.class);
		}
		public Math_expressionContext math_expression(int i) {
			return getRuleContext(Math_expressionContext.class,i);
		}
		public OperatorContext operator() {
			return getRuleContext(OperatorContext.class,0);
		}
		public TermContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_term; }
	}

	public final TermContext term() throws RecognitionException {
		TermContext _localctx = new TermContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_term);
		try {
			setState(36);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,1,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(28);
				match(T__0);
				setState(29);
				expression();
				setState(30);
				match(T__1);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(32);
				math_expression();
				setState(33);
				operator();
				setState(34);
				math_expression();
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
	public static class Math_expressionContext extends ParserRuleContext {
		public List<Math_termContext> math_term() {
			return getRuleContexts(Math_termContext.class);
		}
		public Math_termContext math_term(int i) {
			return getRuleContext(Math_termContext.class,i);
		}
		public List<Math_operatorContext> math_operator() {
			return getRuleContexts(Math_operatorContext.class);
		}
		public Math_operatorContext math_operator(int i) {
			return getRuleContext(Math_operatorContext.class,i);
		}
		public Math_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_math_expression; }
	}

	public final Math_expressionContext math_expression() throws RecognitionException {
		Math_expressionContext _localctx = new Math_expressionContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_math_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(38);
			math_term();
			setState(44);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,2,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					{
					setState(39);
					math_operator();
					}
					setState(40);
					math_term();
					}
					} 
				}
				setState(46);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,2,_ctx);
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
	public static class Math_termContext extends ParserRuleContext {
		public Math_expressionContext math_expression() {
			return getRuleContext(Math_expressionContext.class,0);
		}
		public TerminalNode IDENTIFIER() { return getToken(PseudoResultExpressionParser.IDENTIFIER, 0); }
		public TerminalNode NUMBER() { return getToken(PseudoResultExpressionParser.NUMBER, 0); }
		public Math_termContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_math_term; }
	}

	public final Math_termContext math_term() throws RecognitionException {
		Math_termContext _localctx = new Math_termContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_math_term);
		try {
			setState(61);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__0:
				enterOuterAlt(_localctx, 1);
				{
				setState(47);
				match(T__0);
				setState(48);
				math_expression();
				setState(49);
				match(T__1);
				}
				break;
			case T__2:
				enterOuterAlt(_localctx, 2);
				{
				setState(51);
				match(T__2);
				setState(52);
				math_expression();
				setState(53);
				match(T__1);
				}
				break;
			case T__3:
				enterOuterAlt(_localctx, 3);
				{
				setState(55);
				match(T__3);
				setState(56);
				math_expression();
				}
				break;
			case T__4:
				enterOuterAlt(_localctx, 4);
				{
				setState(57);
				match(T__4);
				setState(58);
				math_expression();
				}
				break;
			case IDENTIFIER:
				enterOuterAlt(_localctx, 5);
				{
				setState(59);
				match(IDENTIFIER);
				}
				break;
			case NUMBER:
				enterOuterAlt(_localctx, 6);
				{
				setState(60);
				match(NUMBER);
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
	public static class OperatorContext extends ParserRuleContext {
		public OperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_operator; }
	}

	public final OperatorContext operator() throws RecognitionException {
		OperatorContext _localctx = new OperatorContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(63);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 4032L) != 0)) ) {
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
	public static class Logical_operatorContext extends ParserRuleContext {
		public Logical_operatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_logical_operator; }
	}

	public final Logical_operatorContext logical_operator() throws RecognitionException {
		Logical_operatorContext _localctx = new Logical_operatorContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_logical_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(65);
			_la = _input.LA(1);
			if ( !(_la==T__11 || _la==T__12) ) {
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
	public static class Math_operatorContext extends ParserRuleContext {
		public Math_operatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_math_operator; }
	}

	public final Math_operatorContext math_operator() throws RecognitionException {
		Math_operatorContext _localctx = new Math_operatorContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_math_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(67);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 49200L) != 0)) ) {
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
		"\u0004\u0001\u0013F\u0002\u0000\u0007\u0000\u0002\u0001\u0007\u0001\u0002"+
		"\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002\u0004\u0007\u0004\u0002"+
		"\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002\u0007\u0007\u0007\u0001"+
		"\u0000\u0001\u0000\u0001\u0000\u0001\u0001\u0001\u0001\u0001\u0001\u0001"+
		"\u0001\u0005\u0001\u0018\b\u0001\n\u0001\f\u0001\u001b\t\u0001\u0001\u0002"+
		"\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002"+
		"\u0001\u0002\u0003\u0002%\b\u0002\u0001\u0003\u0001\u0003\u0001\u0003"+
		"\u0001\u0003\u0005\u0003+\b\u0003\n\u0003\f\u0003.\t\u0003\u0001\u0004"+
		"\u0001\u0004\u0001\u0004\u0001\u0004\u0001\u0004\u0001\u0004\u0001\u0004"+
		"\u0001\u0004\u0001\u0004\u0001\u0004\u0001\u0004\u0001\u0004\u0001\u0004"+
		"\u0001\u0004\u0003\u0004>\b\u0004\u0001\u0005\u0001\u0005\u0001\u0006"+
		"\u0001\u0006\u0001\u0007\u0001\u0007\u0001\u0007\u0000\u0000\b\u0000\u0002"+
		"\u0004\u0006\b\n\f\u000e\u0000\u0003\u0001\u0000\u0006\u000b\u0001\u0000"+
		"\f\r\u0002\u0000\u0004\u0005\u000e\u000fE\u0000\u0010\u0001\u0000\u0000"+
		"\u0000\u0002\u0013\u0001\u0000\u0000\u0000\u0004$\u0001\u0000\u0000\u0000"+
		"\u0006&\u0001\u0000\u0000\u0000\b=\u0001\u0000\u0000\u0000\n?\u0001\u0000"+
		"\u0000\u0000\fA\u0001\u0000\u0000\u0000\u000eC\u0001\u0000\u0000\u0000"+
		"\u0010\u0011\u0003\u0002\u0001\u0000\u0011\u0012\u0005\u0000\u0000\u0001"+
		"\u0012\u0001\u0001\u0000\u0000\u0000\u0013\u0019\u0003\u0004\u0002\u0000"+
		"\u0014\u0015\u0003\f\u0006\u0000\u0015\u0016\u0003\u0004\u0002\u0000\u0016"+
		"\u0018\u0001\u0000\u0000\u0000\u0017\u0014\u0001\u0000\u0000\u0000\u0018"+
		"\u001b\u0001\u0000\u0000\u0000\u0019\u0017\u0001\u0000\u0000\u0000\u0019"+
		"\u001a\u0001\u0000\u0000\u0000\u001a\u0003\u0001\u0000\u0000\u0000\u001b"+
		"\u0019\u0001\u0000\u0000\u0000\u001c\u001d\u0005\u0001\u0000\u0000\u001d"+
		"\u001e\u0003\u0002\u0001\u0000\u001e\u001f\u0005\u0002\u0000\u0000\u001f"+
		"%\u0001\u0000\u0000\u0000 !\u0003\u0006\u0003\u0000!\"\u0003\n\u0005\u0000"+
		"\"#\u0003\u0006\u0003\u0000#%\u0001\u0000\u0000\u0000$\u001c\u0001\u0000"+
		"\u0000\u0000$ \u0001\u0000\u0000\u0000%\u0005\u0001\u0000\u0000\u0000"+
		"&,\u0003\b\u0004\u0000\'(\u0003\u000e\u0007\u0000()\u0003\b\u0004\u0000"+
		")+\u0001\u0000\u0000\u0000*\'\u0001\u0000\u0000\u0000+.\u0001\u0000\u0000"+
		"\u0000,*\u0001\u0000\u0000\u0000,-\u0001\u0000\u0000\u0000-\u0007\u0001"+
		"\u0000\u0000\u0000.,\u0001\u0000\u0000\u0000/0\u0005\u0001\u0000\u0000"+
		"01\u0003\u0006\u0003\u000012\u0005\u0002\u0000\u00002>\u0001\u0000\u0000"+
		"\u000034\u0005\u0003\u0000\u000045\u0003\u0006\u0003\u000056\u0005\u0002"+
		"\u0000\u00006>\u0001\u0000\u0000\u000078\u0005\u0004\u0000\u00008>\u0003"+
		"\u0006\u0003\u00009:\u0005\u0005\u0000\u0000:>\u0003\u0006\u0003\u0000"+
		";>\u0005\u0010\u0000\u0000<>\u0005\u0011\u0000\u0000=/\u0001\u0000\u0000"+
		"\u0000=3\u0001\u0000\u0000\u0000=7\u0001\u0000\u0000\u0000=9\u0001\u0000"+
		"\u0000\u0000=;\u0001\u0000\u0000\u0000=<\u0001\u0000\u0000\u0000>\t\u0001"+
		"\u0000\u0000\u0000?@\u0007\u0000\u0000\u0000@\u000b\u0001\u0000\u0000"+
		"\u0000AB\u0007\u0001\u0000\u0000B\r\u0001\u0000\u0000\u0000CD\u0007\u0002"+
		"\u0000\u0000D\u000f\u0001\u0000\u0000\u0000\u0004\u0019$,=";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}